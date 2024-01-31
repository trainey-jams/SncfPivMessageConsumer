using Amazon.SQS;

namespace PIV_POC_Client.Interfaces
{
    public interface ISqsClientFactory
    {
        public IAmazonSQS GetSqsClient();
        
        public string GetSqsQueue();
    }
}
