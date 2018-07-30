using System;

namespace SCC.NodeCommunication
{
    internal interface IProtocolInfo
    {
        Guid Guid { get; }
        Type Protocol { get; }
    }
}