using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SncfPivMessageConsumer.Interfaces;
using SncfPivMessageConsumer.Models.Config;
using Polly;
using Polly.CircuitBreaker;
using System.Net;

namespace SncfPivMessageConsumer.AWS.Repos
{
    public class SnsRepository : ISnsRepository
    {
        private readonly ILogger<SnsRepository> Logger;
        private readonly SnsConfig SnsConfig;
        private readonly IAmazonSimpleNotificationService SnsClient;
        private readonly IAsyncPolicy Policy;

        public SnsRepository(ILogger<SnsRepository> logger, IOptions<SnsConfig> sqsConfig, IAmazonSimpleNotificationService snsClient, IAsyncPolicy policy)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            SnsConfig = sqsConfig.Value ?? throw new ArgumentNullException(nameof(sqsConfig));
            SnsClient = snsClient ?? throw new ArgumentNullException(nameof(snsClient));
            Policy = policy ?? throw new ArgumentNullException(nameof(policy));
        }

        public async Task<bool> PublishMessage(string message)
        {
            message = message.Replace(" ", "").Replace("\r\n", "");

            try
            {
                var request = new PublishRequest
                {
                    Message = message,
                    TopicArn = SnsConfig.GetSnsTopic(),
                    MessageGroupId = "PIVPublisher101"
                };

                var response = await Policy.ExecuteAsync(async () => await SnsClient.PublishAsync(request));

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                return false;
            }
            catch (AmazonSimpleNotificationServiceException ex)
            {
                Logger.LogError(ex, $"Failed to write to SNS queue {SnsConfig.TopicName}");
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
