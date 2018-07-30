using SCC.DataModel;
using System;
using System.Collections.Generic;

namespace SCC.NodeCommunication
{
    internal class Peer
    {
        private Server server;

        private readonly List<IProtocol> connections;

        public Peer(Server server)
        {
            this.server = server;
            server.Datastore.entryList.AddingNew += EntryList_AddingNew;

            this.connections = new List<IProtocol>();
        }

        private void EntryList_AddingNew(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            var entry = (e.NewObject) as Entry;

            var entryAdded = new EntryAdded(entry);
            SendRequest(entryAdded, null);
        }

        public Guid Guid { get; set; }

        internal void AddConnection(IProtocol protocol)
        {

            protocol.Closed += Protocol_Closed;
            protocol.RequestRecieved += Protocol_RequestRecieved;
            protocol.Start();

            connections.Add(protocol);
        }

        private void Protocol_RequestRecieved(IRequest request, Action<Result, IResponse> respond)
        {
            // Select Protocol Type

            var requestType = request.GetType();

            try
            {
                if (requestType == typeof(Get))
                {
                    // Generate Reply
                    var get = request as Get;

                    IEntry entry = server.Datastore.GetEntry((Entry) get.Entry);

                    respond(Result.NoError, new Get.Response(entry));
                }
                else if (requestType == typeof(EntryAdded))
                {
                    var entryAdded = request as EntryAdded;

                    // No response for this type
                    
                    if(!server.Datastore.Exists(entryAdded.Entry))
                    {
                        var get = new Get(entryAdded.Entry);
                        SendRequest((IRequest) get 
                            ,(IResponse response)  =>
                                {
                                    var responseType = response.GetType();
                                    try
                                    {
                                        if (responseType != typeof(Get.Response))
                                            return;

                                        var getR = response as Get.Response;

                                        IEntry entry = server.Datastore.AddEntry(getR.Entry);
                                    }
                                    catch
                                    {

                                    }
                                });
                    }
                }
                else
                {
                    // Unsupported
                    respond(Result.UnsupportedRequest, null);
                }
            }
            catch
            {
                // Unknown Error
                respond(Result.UnknownError, null);
            }
        }

        private void SendRequest(IRequest get, Action<IResponse> respond)
        {
            throw new NotImplementedException();
        }

        private void Protocol_Closed(IProtocol obj)
        {
            this.connections.Remove(obj);
        }
    }
}