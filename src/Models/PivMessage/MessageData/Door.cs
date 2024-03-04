using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Door
    {
        [JsonProperty("position")]
        public string Position { get; set; } = string.Empty;
    }
}
