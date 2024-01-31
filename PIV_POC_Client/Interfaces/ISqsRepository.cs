using Amazon.SQS.Model;

namespace PIV_POC_Client.Interfaces
{
    public interface ISqsRepository
    {
        Task PublishMessage(string message);

        Task PublishMessageBatch(List<SendMessageBatchRequestEntry> messages);
    }
}