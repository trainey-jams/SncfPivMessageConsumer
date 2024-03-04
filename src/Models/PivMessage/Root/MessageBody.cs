using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.Root
{
    public class MessageBody
    {
        [JsonProperty("messageTime")]
        public DateTime MessageDate { get; set; }

        [JsonProperty("objects")]
        public List<PivMessageObject> MessageObjects { get; set; } = new List<PivMessageObject>();
    }
}
