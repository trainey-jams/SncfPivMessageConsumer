using Amazon;
using Amazon.SQS;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;
using PIV_POC_Client.Models.Config.AWS;

namespace PIV_POC_Client.AWS.ClientFactories.SQS
{
    public class SqsClientFactory : ISqsClientFactory
    {
        private readonly AWSCredentialConfig Credentials;
        private readonly SqsConfig SqsConfig;

        public SqsClientFactory(IOptions<AWSCredentialConfig> credentials, IOptions<SqsConfig> sqsConfig)
        {
            Credentials = credentials.Value;
            SqsConfig = sqsConfig.Value;
        }

        public IAmazonSQS GetSqsClient()
        {
            var config = new AmazonSQSConfig
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(SqsConfig.Region),
                ServiceURL = $"https://sqs.{SqsConfig.Region}.amazonaws.com"
            };

            return new AmazonSQSClient(Credentials.AccessKey, Credentials.SecretKey, Credentials.SessionToken, config);
        }

        public string GetSqsQueue() =>
            $"https://sqs.{SqsConfig.Region}.amazonaws.com/{SqsConfig.SqsQueueId}/{SqsConfig.SqsQueueName}";
    }
}
