using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using System.Linq.Expressions;
using System.Net;

namespace PIV_POC_Client.AWS.Repos
{
    public class SqsRepository : ISqsRepository
    {
        private readonly IAmazonSQS SqsClient;
        private readonly SqsConfig SqsConfig;

        public SqsRepository(IAmazonSQS sqsClient, IOptions<SqsConfig> sqsConfig)
        {
            SqsClient = sqsClient ?? throw new ArgumentNullException(nameof(sqsClient));
            SqsConfig = sqsConfig.Value ?? throw new ArgumentNullException(nameof(sqsConfig));
        }

        public async Task<bool> PublishMessage(string message)
        {
            var request = new SendMessageRequest
            {
                MessageBody = message,
                QueueUrl = SqsConfig.GetSqsQueue(),
            };

            var response = await SqsClient.SendMessageAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
