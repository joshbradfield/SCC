using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCC.DataModel
{
    public class Datastore
    {
        public Datastore()
        {
            entryList = new BindingList<Entry>();
        }

        private readonly SynchronizationContext _context = SynchronizationContext.Current;

        public BindingList<Entry> entryList { get; }

        public void Add(Entry entry)
        {
            _context.Post(delegate
            {
                entryList.Add(entry);
            }, null);
        }

        public bool Exists(Entry entry)
        {
            return entryList.Any((x =>(x.Guid == entry.Guid)));
        }

        public static IEntry FromByteArray(Guid table, byte[] serialised)
        {
            throw new NotImplementedException();
        }

        public IEntry GetEntry(Entry entry)
        {
            throw new NotImplementedException();
        }

        public IEntry AddEntry(IEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
