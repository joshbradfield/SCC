using System;
using System.Net.Sockets;

namespace SCC.NodeCommunication
{

    enum Result
    {
          NoError
        , Timeout
        , UknownRequest
        , UnsupportedRequest
        , UnknownError
        , Refused
    }

    internal interface IProtocol
    {
        void Initalise(NetworkStream networkStream, Guid hostGuid, Action<bool, Guid> callback);

        void Start();
        void Stop();

        void SendRequest(IRequest request, Action<Result, IResponse> response);

        Guid Guid { get; }

        event Action<IProtocol> Closed;
        event Action<IRequest, Action<Result, IResponse>> RequestRecieved;
    }
}