using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.Root
{
    public class PivMessageRoot
    {
        public string Expiration { get; set; } = string.Empty;

        public long BrokerInTime { get; set; }

        public long BrokerOutTime { get; set; }

        public string Destination { get; set; } = string.Empty;

        public string MessageId { get; set; } = string.Empty;

        public byte Priority { get; set; }

        [JsonProperty("content")]
        public MessageBody MessageBody { get; set; } = new MessageBody();
    }

    public class MessageHeaders
    {
        [JsonProperty("content-length")]
        public int ContentLength { get; set; }

        [JsonProperty("expires")]
        public string Expires { get; set; } = string.Empty;

        [JsonProperty("destination")]
        public string Destination { get; set; } = string.Empty;

        [JsonProperty("message-id")]
        public string MessageId { get; set; } = string.Empty;

        [JsonProperty("priority")]
        public string Priority { get; set; } = string.Empty;

        [JsonProperty("subscription")]
        public string SubscriptionId { get; set; } = string.Empty;
    }

    public class MessageBody
    {
        [JsonProperty("messageTime")]
        public DateTime MessageDate { get; set; }

        [JsonProperty("objects")]
        public List<PivMessageObject> MessageObjects { get; set; } = new List<PivMessageObject>();
    }
}
