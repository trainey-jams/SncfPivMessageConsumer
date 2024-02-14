using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using System.Net;

namespace PIV_POC_Client.AWS.Repos
{
    public class SqsRepository : ISqsRepository
    {
        private readonly ILogger<SqsRepository> Logger;
        private readonly SqsConfig SqsConfig;
        private readonly IAmazonSQS SqsClient;

        public SqsRepository(ILogger<SqsRepository> logger, IOptions<SqsConfig> sqsConfig, IAmazonSQS sqsClient)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            SqsConfig = sqsConfig.Value ?? throw new ArgumentNullException(nameof(sqsConfig));
            SqsClient = sqsClient ?? throw new ArgumentNullException(nameof(sqsClient));
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

                var response = await SqsClient.SendMessageAsync(request);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);

                return false;
            }
        }
    }
}
