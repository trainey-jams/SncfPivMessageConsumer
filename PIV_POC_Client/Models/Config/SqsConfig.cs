namespace PIV_POC_Client.Models.Config
{
    public class SqsConfig
    {
        public string SqsRegion { get; set; } = string.Empty;
        
        public string SqsQueueId { get; set; } = string.Empty;

        public string SqsQueueName { get; set; } = string.Empty;

        public string AccessKey { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;

        public string SessionToken { get; set; } = string.Empty;

    }
}
