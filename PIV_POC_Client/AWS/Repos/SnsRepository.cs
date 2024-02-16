﻿using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SQS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using Polly;
using Polly.CircuitBreaker;
using System.Net;

namespace PIV_POC_Client.AWS.Repos
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
            string y = message.Replace("\r\n", "");

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
                Logger.LogError(ex, $"Failed to write to SNS queue {SnsConfig.SnsTopicName}");
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
