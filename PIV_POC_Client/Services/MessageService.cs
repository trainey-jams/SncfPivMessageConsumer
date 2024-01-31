using Amazon.SQS.Model;
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

        public async Task<SendMessageBatchRequestEntry> ProcessMessage(ClientWebSocket client, Guid subscriptionId)
        {
            SendMessageBatchRequestEntry result = new SendMessageBatchRequestEntry();
            try
            {
                string rawMessage = await ServerFrameWrapper.ReceiveMessage(client);

                Console.WriteLine($"This messages has been processed by {subscriptionId}");

                if (string.IsNullOrWhiteSpace(rawMessage))
                {
                    return result;
                }

                result.Id = Guid.NewGuid().ToString();
                result.MessageBody = await MessageProcessor.Process(subscriptionId, rawMessage);;


                return result;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());

                return result;
            }
        }
    }
}
