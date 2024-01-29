using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config.STOMP;
using PIV_POC_Client.Models.Enums;
using System.Net.WebSockets;
using System.Text;

namespace PIV_POC_Client.STOMP.Wrappers
{
    public class StompClientFrameWrapper : IStompClientFrameWrapper
    {
        const string MessageEnd = "\n\n\0";

        public async Task Connect(ClientWebSocket webSocket, ConnectConfiguration connectConfiguration)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                string conn = 
                    $"CONNECT" +
                    $"\nlogin:{connectConfiguration.Login}" +
                    $"\npasscode:{connectConfiguration.Passcode}" +
                    $"\naccept-version:{connectConfiguration.AcceptVersion}" +
                    $"\nheart­-beat:{connectConfiguration.IncomingHeartbeatMs},{connectConfiguration.OutgoingHeartbeatMs}" +
                    $"\nclientid:{connectConfiguration.ClientId}"+
                    $"{MessageEnd}";

                byte[] connbytes = Encoding.UTF8.GetBytes(conn);

                await webSocket.SendAsync(connbytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task Disconnect(ClientWebSocket webSocket, Guid receiptId)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                string conn = 
                    $"DISCONNECT" +
                    $"\nreceipt:{receiptId}{MessageEnd}";

                byte[] connbytes = Encoding.UTF8.GetBytes(conn);

                await webSocket.SendAsync(connbytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task Subscribe(ClientWebSocket webSocket, SubscribeConfiguration subscribeConfiguration)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                string subs =
                    $"SUBSCRIBE" +
                    $"\nid:{subscribeConfiguration.SubscriptionId}" +
                    $"\ndestination:{subscribeConfiguration.Destination}";

                subs += $"\nack:{subscribeConfiguration.GetAckowledgementMode()}";

                if (subscribeConfiguration.SubscriptionType == SubscriptionType.Durable)
                {
                    subs += $"\nactivemq.subscriptionName:{subscribeConfiguration.SubscriptionName}";
                }

                subs += $"{MessageEnd}";

                byte[] subsbytes = Encoding.UTF8.GetBytes(subs);

                await webSocket.SendAsync(subsbytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task Unsubscribe(ClientWebSocket webSocket, Guid subscriptionId)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                string subs = 
                    $"UNSUBSCRIBE" +
                    $"\nid:{subscriptionId}{MessageEnd}";

                byte[] subsbytes = Encoding.UTF8.GetBytes(subs);

                await webSocket.SendAsync(subsbytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task Acknowledge(ClientWebSocket webSocket, Guid messageAckHeader)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                string subs =
                    $"ACK" +
                    $"\nid:{messageAckHeader}" +
                    $"{MessageEnd}";

                byte[] subsbytes = Encoding.UTF8.GetBytes(subs);

                await webSocket.SendAsync(subsbytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
