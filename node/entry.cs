using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace node
{
    [Serializable()]
    public class Entry
    {
        public Entry()
        {
            this.DateTime = DateTime.UtcNow;
            this.Guid = Guid.NewGuid();
        }

        public Entry(byte[] serialised)
        {

        }

        public Entry(DateTime dateTime, Guid guid)
        {
            this.DateTime = dateTime;
            this.Guid = guid;
        }
       

        public DateTime DateTime
        {
            get;
            private set;
        }
        
        public Guid Guid
        {
            get;
            private set;
        }

        public byte[] ToByteArray()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (System.IO.MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                return ms.ToArray();
            }
        }

        public static Entry FromByteArray(byte[] array)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (System.IO.MemoryStream ms = new MemoryStream())
            {
                ms.Write(array, 0, array.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return (Entry) bf.Deserialize(ms);
            }
        }
    }
}
