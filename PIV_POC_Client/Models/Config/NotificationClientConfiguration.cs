using PIV_POC_Client.Models.Config.STOMP;
using PIV_POC_Client.Models.Config.Websocket;

namespace PIV_POC_Client.Models.Config
{
    public class NotificationClientConfiguration
    {   
        public Guid ReceiptId { get; set; }

        public ConnectConfiguration ConnectConfiguration { get; set; } = new ConnectConfiguration();

        public SubscribeConfiguration SubscribeConfiguration { get; set; } = new SubscribeConfiguration();

        public WebsocketClientConfiguration WebsocketClientConfiguration { get; set; } = new WebsocketClientConfiguration();
    }
}
