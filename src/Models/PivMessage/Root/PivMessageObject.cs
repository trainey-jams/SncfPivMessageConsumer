using Newtonsoft.Json;
using SncfPivMessageConsumer.Models.PivMessage.MessageData;
using System.Text.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.Root
{
    public class PivMessageObject
    {
        public string ObjectId { get; set; } = string.Empty;

        [JsonProperty("Object")]
        public PivMessageData MessageData { get; set; } = new PivMessageData();

        public Properties Properties { get; set; } = new Properties();
    }
}
