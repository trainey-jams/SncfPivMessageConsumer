using Amazon.SQS.ExtendedClient;
using Amazon.SQS.Model;
using PIV_POC_Client.AWS.ClientFactories.S3;
using PIV_POC_Client.Interfaces;

namespace PIV_POC_Client.AWS.Repos
{
    public class S3Repository : IS3Repository
    {
        private readonly IS3ClientFactory S3ClientFactory;
        private readonly ISqsClientFactory SqsClientFactory;

        public S3Repository(IS3ClientFactory s3ClientFactory, ISqsClientFactory sqsClientFactory)
        {
            S3ClientFactory = s3ClientFactory ?? throw new ArgumentNullException(nameof(s3ClientFactory));
            SqsClientFactory = sqsClientFactory ?? throw new ArgumentNullException(nameof(sqsClientFactory));
        }

        public async Task SendMessageToS3(string message)
        {
            var extendedClient = new AmazonSQSExtendedClient(
            SqsClientFactory.GetSqsClient(),
            new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(S3ClientFactory.GetS3Client(), "piv-message-bucket-s3"));

            await extendedClient.SendMessageAsync(SqsClientFactory.GetSqsQueue(), message);
        }

        public async Task SendMessageBatchToS3(List<SendMessageBatchRequestEntry> messageBatch)
        {
            var extendedClient = new AmazonSQSExtendedClient(
            SqsClientFactory.GetSqsClient(),
            new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(S3ClientFactory.GetS3Client(), "piv-message-bucket-s3"));

            SendMessageBatchRequest batchRequest = new SendMessageBatchRequest
            {
                Entries = messageBatch,
                QueueUrl = SqsClientFactory.GetSqsQueue()
            };

            await extendedClient.SendMessageBatchAsync(batchRequest);
        }
    }
}
