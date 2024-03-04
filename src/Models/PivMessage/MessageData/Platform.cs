using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Platform
    {
        [JsonProperty("numero")]
        public string Number { get; set; } = string.Empty;
    }
}
