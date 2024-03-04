using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Presentation
    {
        [JsonProperty("codeCouleur")]
        public string ColourCode { get; set; } = string.Empty;
    }
}
