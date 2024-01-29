using PIV_POC_Client.Interfaces;
using System.Net.WebSockets;
using System.Text;

namespace PIV_POC_Client.STOMP.Wrappers
{
    public class StompServerFrameWrapper : IStompServerFrameWrapper
    {
        private readonly IMessageProcessor MessageParser;

        public StompServerFrameWrapper( IMessageProcessor messageParser)
        {
            MessageParser = messageParser ?? throw new ArgumentNullException(nameof(messageParser));
        }

        public async Task<string> ReceiveMessage(ClientWebSocket client)
        {
            var buffer = new ArraySegment<byte>(new byte[2000000]);

            WebSocketReceiveResult result;
            using (var ms = new MemoryStream())
            {
                do
                {
                    result = await client.ReceiveAsync(buffer, CancellationToken.None);

                    ms.Write(buffer.Array, buffer.Offset, result.Count);

                } while (!result.EndOfMessage);

                if (result.MessageType == WebSocketMessageType.Close)

                    return string.Empty;

                ms.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    string msg = await reader.ReadToEndAsync();

                    return msg;
                }
            }
        }
    }
}

