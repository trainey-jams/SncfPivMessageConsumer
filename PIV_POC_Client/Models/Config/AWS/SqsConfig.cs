namespace PIV_POC_Client.Models.Config
{
    public class SqsConfig
    {
        public string Region { get; set; } = string.Empty;
        
        public string SqsQueueId { get; set; } = string.Empty;

        public string SqsQueueName { get; set; } = string.Empty;

        public string GetSqsQueue() =>
           $"https://sqs.{Region}.amazonaws.com/{SqsQueueId}/{SqsQueueName}";
    }
}
