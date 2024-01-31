using Amazon.SQS.Model;

namespace PIV_POC_Client.AWS.Repos
{
    public interface IS3Repository
    {
        Task SendMessageBatchToS3(List<SendMessageBatchRequestEntry> messageBatch);
        Task SendMessageToS3(string message);
    }
}