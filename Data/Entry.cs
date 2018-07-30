using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SSC.DataModel;
using System.Net.NetworkInformation;

namespace SCC.DataModel
{
    public class Entry : IEntry
    {
        public Entry()
        {
            this.Guid = GuidGenerator.GenerateTimeBasedGuid();
        }

        public Entry(byte[] serialised)
        {
            var guid = serialised.Take(8).ToArray();
            var table = serialised.Skip(8).ToArray();

            this.Guid = new System.Guid(guid);
            if ( Guid.GetUuidVersion() != GuidVersion.TimeBased )
            {
                throw new ArgumentOutOfRangeException("serialised", "Guid MUST be 'Time Based' (v1).");
            }

            this.Table = new System.Guid(table);

            if (Guid.GetUuidVersion() != GuidVersion.Random)
            {
                throw new ArgumentOutOfRangeException("serialised", "Table MUST be a 'Random' (v4) Guid.");
            }
        }

        public Entry(Guid guid, Guid table)
        {
            this.Guid = guid;
            this.Table = table;

            if (Guid.GetUuidVersion() != GuidVersion.TimeBased)
            {
                throw new ArgumentOutOfRangeException("guid", "Guid MUST be 'Time Based' (v1).");
            }


            if (Table.GetUuidVersion() != GuidVersion.Random)
            {
                throw new ArgumentOutOfRangeException("table", "Table MUST be a 'Random' (v4) Guid.");
            }
        }
        
        public DateTime DateTime => GuidGenerator.GetDateTime(Guid);

        public Guid Guid { get; private set; }
        public Guid Table { get; private set; }

        public byte[] ToByteArray()
        {
            return Guid.ToByteArray().Concat(Table.ToByteArray()).ToArray();
        }
        
    }
}
