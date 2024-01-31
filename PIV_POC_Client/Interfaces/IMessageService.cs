using Amazon.SQS.Model;
using System.Net.WebSockets;

namespace PIV_POC_Client.Interfaces
{
    public interface IMessageService
    {
        Task<SendMessageBatchRequestEntry> ProcessMessage(ClientWebSocket client, Guid subscriptionId);
    }
}
