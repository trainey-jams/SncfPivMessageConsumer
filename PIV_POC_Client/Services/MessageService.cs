using Amazon.SQS.Model;
using PIV_POC_Client.Interfaces;
using System.Net.WebSockets;

namespace PIV_POC_Client.Services
{
    public class MessageService : IMessageService
    {
        public MessageService()
        {
        }

        public async Task<SendMessageBatchRequestEntry> ProcessMessage(ClientWebSocket client, Guid subscriptionId)
        {
            SendMessageBatchRequestEntry result = new SendMessageBatchRequestEntry();
            try
            {
                //string rawMessage = await ServerFrameWrapper.ReceiveMessage(client);

                //Console.WriteLine($"This messages has been processed by {subscriptionId}");

                //if (string.IsNullOrWhiteSpace(rawMessage))
                //{
                //    return result;
                //}

                //result.Id = Guid.NewGuid().ToString();
                //result.MessageBody = await MessageProcessor.Process(subscriptionId, rawMessage);;


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
