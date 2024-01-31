using Amazon.SQS.Model;
using PIV_POC_Client.Interfaces;
using System.Linq.Expressions;

namespace PIV_POC_Client.AWS.Repos
{
    public class SqsRepository : ISqsRepository
    {
        private readonly ISqsClientFactory _sqsClientFactory;

        public SqsRepository(ISqsClientFactory sqsClientFactory)
        {
            _sqsClientFactory = sqsClientFactory ?? throw new ArgumentNullException(nameof(sqsClientFactory));
        }

        public async Task PublishMessage(string message)
        {
            message = message.Replace('\x0', ' ');

            var request = new SendMessageRequest
            {
                MessageBody = message,
                QueueUrl = _sqsClientFactory.GetSqsQueue(),
            };

            var client = _sqsClientFactory.GetSqsClient();

            var response = await client.SendMessageAsync(request);
        }

        public async Task PublishMessageBatch(List<SendMessageBatchRequestEntry> messages)
        {
            //  message = message.Replace('\x0', ' ');

            var request = new SendMessageBatchRequest
            {
                Entries = messages,
                QueueUrl = _sqsClientFactory.GetSqsQueue()
            };


            var client = _sqsClientFactory.GetSqsClient();

            var response = await client.SendMessageBatchAsync(request);
        }
    }
}
