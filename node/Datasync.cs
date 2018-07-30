using SCC.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SCC.Communication
{
    public class Datasync
    {
        public Datasync(Datastore datastore, int port = 35830)
        {
            this.DataStore = datastore;
            datastore.entryList.ListChanged += EntryList_ListChanged;
            this.Port = port;
            this.UdpClient = new UdpClient();
            this.UdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port + 1));
            this.UdpClient.BeginReceive(DataReceived, this);
        }

        private void EntryList_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if(e.ListChangedType == System.ComponentModel.ListChangedType.ItemAdded)
            {
                var array = this.DataStore.entryList[e.NewIndex].ToByteArray();

                UdpClient.BeginSend(array, array.Length, new IPEndPoint(IPAddress.Broadcast, Port), DataSent, this);
            }
        }

        private void DataSent(IAsyncResult ar)
        {
            
        }

        private void DataReceived(IAsyncResult ar)
        {
            IPEndPoint e = new IPEndPoint(IPAddress.Any, Port + 1);
            Byte[] receiveBytes = UdpClient.EndReceive(ar, ref e);

            var entry = new Entry(receiveBytes);

            if (!DataStore.Exists(entry))
                DataStore.Add(entry);

            this.UdpClient.BeginReceive(DataReceived, this);

        }

        public Datastore DataStore { get; protected set; }
        public int Port { get; }

        protected UdpClient UdpClient;
    }
}
