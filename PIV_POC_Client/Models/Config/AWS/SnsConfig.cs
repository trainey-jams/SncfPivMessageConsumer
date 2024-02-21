namespace PIV_POC_Client.Models.Config
{
    public class SnsConfig
    {
        public string Region { get; set; } = string.Empty;

        public string QueueId {  get; set; } = string.Empty;
        
        public string SnsTopicName { get; set; } = string.Empty;

        public string GetSnsTopic() =>
           $"arn:aws:sns:{Region}:{QueueId}:{SnsTopicName}.fifo";
    }
}
