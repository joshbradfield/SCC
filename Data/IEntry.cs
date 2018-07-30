using System;

namespace SCC.DataModel
{
    public interface IEntry
    {
        Guid Guid { get; }
        Guid Table { get; }
        byte[] ToByteArray();
    }
}