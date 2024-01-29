using PIV_POC_Client.Models.Config.STOMP;
using System.Net.WebSockets;

namespace PIV_POC_Client.Interfaces
{
    public interface IStompClientFrameWrapper
    {
        Task Connect(ClientWebSocket webSocket, ConnectConfiguration credentials);

        Task Disconnect(ClientWebSocket webSocket, Guid receiptId);

        Task Subscribe(ClientWebSocket webSocket, SubscribeConfiguration configuration);

        Task Unsubscribe(ClientWebSocket webSocket, Guid subscriptionId);

        Task Acknowledge(ClientWebSocket webSocket, Guid messageAckHeader);
    }
}
