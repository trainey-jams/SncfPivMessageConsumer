using System.Net.WebSockets;

namespace PIV_POC_Client.STOMP.Wrappers
{
    public interface IStompServerFrameWrapper
    {
        public Task<string> ReceiveMessage(ClientWebSocket webSocket);
    }
}
