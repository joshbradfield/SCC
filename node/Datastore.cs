using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace node
{
    public class Datastore :INotifyPropertyChanged
    {
        public Datastore()
        {
            entryList = new BindingList<Entry>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public BindingList<Entry> entryList
        {
            get;
            protected set;
        }

        private readonly SynchronizationContext _context = SynchronizationContext.Current;

        public void Add(Entry entry)
        {
            _context.Post(delegate
            {
                entryList.Add(entry);
            }, null);
        }

        public bool Exists(Entry entry)
        {
            return entryList.Any((x => (x.DateTime == entry.DateTime) && (x.Guid == entry.Guid)));
        }
    }
}
