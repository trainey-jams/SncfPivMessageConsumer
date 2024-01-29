using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Results;
using PIV_POC_Client.STOMP.Wrappers;
using System.Net.WebSockets;

namespace PIV_POC_Client.Services
{
    public class MessageService : IMessageService
    {
        private readonly IStompServerFrameWrapper ServerFrameWrapper;
        private readonly IMessageProcessor MessageProcessor;

        const bool UseSqs = true;

        public MessageService(  
                                IStompServerFrameWrapper serverFrameWrapper, 
                                IMessageProcessor messageProcessor)
        {
            ServerFrameWrapper = serverFrameWrapper ?? throw new ArgumentNullException(nameof(serverFrameWrapper));
            MessageProcessor = messageProcessor ?? throw new ArgumentNullException(nameof(messageProcessor));
        }

        public async Task ProcessMessage(ClientWebSocket client, Guid subscriptionId)
        {
            MessageParseResult result = new MessageParseResult();
            try
            {
                string rawMessage = await ServerFrameWrapper.ReceiveMessage(client);

                Console.WriteLine(rawMessage); 

                if (string.IsNullOrWhiteSpace(rawMessage))
                {
                    return;
                }

                await MessageProcessor.Process(subscriptionId, rawMessage);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
