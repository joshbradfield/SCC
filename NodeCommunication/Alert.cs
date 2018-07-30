using System;
using SCC.DataModel;

namespace SCC.NodeCommunication
{
    internal class EntryAdded : IRequest
    {

        public EntryAdded(Entry entry)
        {
            this.Entry = entry;
        }

        public EntryAdded(byte [] serialised)
        {
            this.Entry = new Entry(serialised);
        }

        public Entry Entry { get; internal set; }

        public Guid Guid => new Guid("dd1719ab-df98-4358-9120-75bc3efd49f0");

        public IResponse ParseResponse(byte[] response) => null;

        public byte[] ToByteArray()
        {
            return Entry.ToByteArray();
        }
    }
}