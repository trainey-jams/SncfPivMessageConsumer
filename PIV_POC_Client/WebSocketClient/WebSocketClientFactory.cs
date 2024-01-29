using System.Net.WebSockets;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config.Websocket;

namespace PIV_POC_Client.WebSocketClient
{
    public class ClientEventArgs : EventArgs
    {
        public ClientWebSocket Client { get; set; }
    }

    public class WebSocketClientFactory : IDisposable, IWebSocketClientFactory
    {
        ClientWebSocket client = new ClientWebSocket();

        public async Task<ClientWebSocket> GetClient(WebsocketClientConfiguration settings)
        {
            if (settings?.SubProtocol != null)
            {
                client.Options.AddSubProtocol(settings.SubProtocol);
            }

            foreach (KeyValuePair<string, string> entry in settings.Headers)
            {
                client.Options.SetRequestHeader(entry.Key, entry.Value);
            }

            await client.ConnectAsync(new Uri(settings.Uri), new CancellationTokenSource().Token);

            return client;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
