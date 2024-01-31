using Amazon;
using Amazon.SQS;
using Microsoft.Extensions.Options;
using PIV_POC_Client.Interfaces;
using PIV_POC_Client.Models.Config;

namespace PIV_POC_Client.AWS.SQS
{
    public class SqsClientFactory : ISqsClientFactory
    {
        private readonly IOptions<SqsConfig> _options;

        public SqsClientFactory(IOptions<SqsConfig> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IAmazonSQS GetSqsClient()
        {
            var config = new AmazonSQSConfig
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_options.Value.SqsRegion),
                ServiceURL = $"https://sqs.{_options.Value.SqsRegion}.amazonaws.com"
            };

            return new AmazonSQSClient(_options.Value.AccessKey, _options.Value.SecretKey, _options.Value.SessionToken, config);
        }

        public string GetSqsQueue() =>
            $"https://sqs.{_options.Value.SqsRegion}.amazonaws.com/{_options.Value.SqsQueueId}/{_options.Value.SqsQueueName}";
    }
}
