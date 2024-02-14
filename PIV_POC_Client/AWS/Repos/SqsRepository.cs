using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using Polly;
using Polly.CircuitBreaker;
using System.Net;

namespace PIV_POC_Client.AWS.Repos
{
    public class SqsRepository : ISqsRepository
    {
        private readonly ILogger<SqsRepository> Logger;
        private readonly SqsConfig SqsConfig;
        private readonly IAmazonSQS SqsClient;
        private readonly IAsyncPolicy Policy;

        public SqsRepository(ILogger<SqsRepository> logger, IOptions<SqsConfig> sqsConfig, IAmazonSQS sqsClient, IAsyncPolicy policy)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            SqsConfig = sqsConfig.Value ?? throw new ArgumentNullException(nameof(sqsConfig));
            SqsClient = sqsClient ?? throw new ArgumentNullException(nameof(sqsClient));
            Policy = policy ?? throw new ArgumentNullException(nameof(policy));
        }

        public async Task<bool> PublishMessage(string messageKey)
        {
            try
            {
                var request = new SendMessageRequest
                {
                    MessageBody = messageKey,
                    QueueUrl = SqsConfig.GetSqsQueue(),
                };

                var response = await Policy.ExecuteAsync(async () => await SqsClient.SendMessageAsync(request));

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                Logger.LogWarning($"Could not write to SQS, key: {messageKey} in queue {SqsConfig.SqsQueueName}");

                return false;
            }
            catch (AmazonSQSException ex)
            {
                Logger.LogError(ex, $"Failed to write to SQS, key: {messageKey} in queue {SqsConfig.SqsQueueName}");
                return false;
            }

            catch (BrokenCircuitException ex)
            {
                Logger.LogError(ex, "S3 circuit open");
                return false;
            }
        }
    }
}
