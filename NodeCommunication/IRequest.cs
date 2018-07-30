using System;

namespace SCC.NodeCommunication
{
    public interface IRequest
    {
        Guid Guid { get; }
        byte[] ToByteArray();
        IResponse ParseResponse(byte[] response);
    }
}