using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.Root
{
    public class PivMessageRoot
    {
        public string Expiration { get; set; } = string.Empty;

        public DateTime BrokerInTime { get; set; }

        public DateTime BrokerOutTime { get; set; }

        public string Destination { get; set; } = string.Empty;

        public string ConversationId { get; set; } = string.Empty;

        public string MessageId { get; set; } = string.Empty;

        public byte Priority { get; set; }

        [JsonProperty("content")]
        public MessageBody MessageBody { get; set; } = new MessageBody();
    }
}