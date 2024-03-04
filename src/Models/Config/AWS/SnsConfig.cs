namespace SncfPivMessageConsumer.Models.Config
{
    public class SnsConfig
    {
        public string Region { get; set; } = string.Empty;

        public string QueueId {  get; set; } = string.Empty;
        
        public string TopicName { get; set; } = string.Empty;

        public string GetSnsTopic() =>
           $"arn:aws:sns:{Region}:{QueueId}:{TopicName}.fifo";
    }
}
