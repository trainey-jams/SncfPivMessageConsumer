using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class ValueItem
    {
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("valeur")]
        public string Value { get; set; } = string.Empty;
    }
}
