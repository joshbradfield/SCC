using SSC.DataModel;
using System;
using System.Net.Sockets;

namespace SCC.NodeCommunication
{
    internal class ProtocolAlpha : IProtocol
    {
        public static readonly Guid Guid = new Guid("4578ee86-cc84-4f45-ab2c-a317a7a6ee7b");

        private NetworkStream networkStream;
        private Guid hostGuid;

        public byte[] readBuffer = new byte[1500];
        public byte[] writeBuffer = new byte[1500];

        public ProtocolAlpha()
        {
        }

        Guid IProtocol.Guid => ProtocolAlpha.Guid;

        public event Action<IProtocol> Closed;
        public event Action<IRequest, Action<Result, IResponse>> RequestRecieved;

       
        public void Initalise(NetworkStream networkStream, Guid hostGuid, Action<bool, Guid> callback)
        {
            this.networkStream = networkStream;
            this.hostGuid = hostGuid;
            
            Array.Copy(Guid.ToByteArray(), 0, readBuffer, 0, 8);
            Array.Copy(hostGuid.ToByteArray(), 0, readBuffer, 8, 8);

            networkStream.BeginWrite(Guid.ToByteArray(), 0, 8
                , (IAsyncResult ar) =>
                    {
                        networkStream.EndWrite(ar);
                        
                        networkStream.BeginRead(readBuffer, 0, 16
                            , (IAsyncResult ar2) =>
                            {
                                bool success = false;
                                Guid peerGuid = Guid.NewGuid();

                                try
                                {
                                    var rec = networkStream.EndRead(ar2);

                                    if ((rec != 16))
                                    {
                                        success = false;
                                        return;
                                    }

                                    var guidBuffer = new byte[8];

                                    Array.Copy(buffer, 0, guidBuffer, 0, 8);

                                    if((new Guid(guidBuffer)) != Guid)
                                    {
                                        success = false;
                                        return;
                                    }

                                    Array.Copy(buffer, 8, guidBuffer, 0, 8);

                                    peerGuid = new Guid(guidBuffer);

                                    if(peerGuid.GetUuidVersion() != GuidVersion.Random)
                                    {
                                        success = false;
                                        return;
                                    }

                                    success = true;

                                }
                                catch
                                {

                                }
                                finally
                                {
                                    callback?.Invoke(success, peerGuid);
                                }

                            }
                            , networkStream);
                    }
                , networkStream); 


        }

        public void SendRequest(IRequest request, Action<Result, IResponse> response)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            networkStream.BeginRead(readBuffer, 0, 8, received, true);
        }

        private void received(IAsyncResult ar)
        {
            try
            {
                int bytesReceived = networkStream.EndRead(ar);

                if (bytesReceived != (8 + 8 + 4 + 1))
                {
                    Close();
                    return;
                }

                var guid = new byte[8];


                Array.Copy(readBuffer, 0, guid, 0, 8);
                if (Guid != new Guid(guid))
                {
                    Close();
                    return;
                }

                Array.Copy(readBuffer, 8, guid, 0, 8);



                var length = BitConverter.ToUInt32(readBuffer, 17);

                networkStream.BeginRead(readBuffer, 0, 8, received, null);
            }
            catch
            {

            }
            
        }

        private void Close()
        {
            networkStream.Close();
            networkStream = null;
            Closed(this);
        }

        public void Stop()
        {
            networkStream = null;
        }
    }
}