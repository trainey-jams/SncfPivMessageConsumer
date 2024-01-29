using PIV_POC_Client.Models.Config.Websocket;
using System.Net.WebSockets;

namespace PIV_POC_Client.Interfaces
{
    public interface IWebSocketClientFactory
    {
        void Dispose();
        Task<ClientWebSocket> GetClient(WebsocketClientConfiguration settings);
    }
}