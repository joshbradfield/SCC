using System;
using SCC.DataModel;

namespace SCC.NodeCommunication
{
    public class Get : IRequest
    {
        private Entry entry;

        public Get(Entry entry)
        {
            this.entry = entry;
        }

        public Entry Entry { get; internal set; }

        public Guid Guid => new Guid("a7c2bbcd-2ebc-4356-bb14-709886a6cc32");

        public IResponse ParseResponse(byte[] serialised)
        {
            return new Get.Response(entry, serialised);
        }

        public byte[] ToByteArray()
        {
            throw new NotImplementedException();
        }

        internal class Response : IResponse
        {
            public Response(IEntry entry)
            {
                Entry = entry;
            }

            public Response(Entry entry, byte[] serialised)
            {
                Entry = Datastore.FromByteArray(entry.Table, serialised);
            }

            public IEntry Entry { get; internal set; }

            public byte[] ToByteArray()
            {
                return Entry.ToByteArray();
            }
        }
    }
}