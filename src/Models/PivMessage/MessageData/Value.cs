using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class ValueItem
    {
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("valeur")]
        public string Value { get; set; } = string.Empty;
    }
}
