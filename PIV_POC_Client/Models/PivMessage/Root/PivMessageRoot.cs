using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.Root
{
    public class PivMessageRoot
    {
        public string Expiration { get; set; } = string.Empty;

        public long BrokerInTime { get; set; }

        public long BrokerOutTime { get; set; }

        public string Destination { get; set; } = string.Empty;

        public string PartitionKey { get; set; } = string.Empty;

        public byte Priority { get; set; }

        [JsonProperty("content")]
        public MessageBody MessageBody { get; set; } = new MessageBody();
    }

    public class MessageBody
    {
        [JsonProperty("messageTime")]
        public DateTime MessageDate { get; set; }

        [JsonProperty("objects")]
        public List<PivMessageObject> MessageObjects { get; set; } = new List<PivMessageObject>();
    }
}
