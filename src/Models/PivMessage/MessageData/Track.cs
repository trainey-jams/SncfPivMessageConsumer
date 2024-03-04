using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Track
    {
        [JsonProperty("numero")]
        public string Number { get; set; } = string.Empty;

        [JsonProperty("numeroQuai")]
        public string PlatformNumber { get; set; } = string.Empty;
    }
}
