using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using SCC.DataModel;

namespace SCC.NodeCommunication
{
    class Server
    {
        public Server(Guid hostGuid, Datastore datastore, int port)
        {
            Datastore = datastore;
            HostGuid = hostGuid;
            Port = port;
            Protocols = new Dictionary<Guid, Type>();
            Peers = new List<Peer>();
        }

        public void Start()
        {
            if (tcpListener == null) return;

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.ExclusiveAddressUse = false;

            tcpListener.Start();

            tcpListener.BeginAcceptTcpClient(IncommingConnectionRequest, tcpListener);

        }

        public void Stop()
        {
            if (tcpListener == null) return;

            tcpListener.Stop();
            tcpListener = null;
        }
         
        private void IncommingConnectionRequest(IAsyncResult ar)
        {
            
            var listener = (TcpListener)ar.AsyncState;

            TcpClient tcpClient = null;

            try
            {
                tcpClient = listener.EndAcceptTcpClient(ar);

                if (tcpClient == null) return;

                if (tcpListener == null)
                {
                    tcpClient.Close();
                    return;
                }

                tcpClient.SendTimeout = 1000;

                var tcpClientStream = tcpClient.GetStream();
                tcpClientStream.ReadTimeout = 1000;

                var protocolGuid = new byte[16];
                tcpClientStream.BeginRead(protocolGuid, 0, protocolGuid.Length
                    , (IAsyncResult ar2) => 
                        {
                            var guid = new Guid(protocolGuid);

                            if(!Protocols.ContainsKey(guid))
                            {
                                tcpClient.Close();
                                return;
                            }

                            IProtocol protocol = (IProtocol) Activator.CreateInstance(Protocols[guid]);


                            protocol.Initalise(HostGuid
                                ,(bool success, Guid peerGuid) =>
                                    {
                                        try
                                        {
                                            if(success)
                                            {
                                                var peer = Peers.Find((x => (x.Guid == peerGuid)));

                                                if(peer == null)
                                                {
                                                    peer = new Peer(this)
                                                    {
                                                        Guid = peerGuid
                                                    };                                                    
                                                }
                                                
                                                peer.AddConnection(protocol);

                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {
                                            if(!success)
                                            {
                                                if (protocol != null)
                                                    protocol.Stop();
                                            }
                                        }

                                    }
                                );

                            // Create and initialise protocol
                                // If protocol accepted, create/add to clientConnection
                                // Else close protocol and close tcp client

                        }
                    , tcpClient);
            }
            catch
            {
                if (tcpClient == null) return;

                tcpClient.Close();
            }
            finally
            {
                if(tcpListener != null)
                    tcpListener.BeginAcceptTcpClient(IncommingConnectionRequest, tcpListener);
            }
        }

        private TcpListener tcpListener;

        public int Port { get; }
        public Dictionary<Guid, Type> Protocols { get; }
        public List<Peer> Peers { get; private set; }
        public Datastore Datastore { get; private set; }
        public Guid HostGuid { get; private set; }

        private readonly Thread connectionThread;
    }
}
