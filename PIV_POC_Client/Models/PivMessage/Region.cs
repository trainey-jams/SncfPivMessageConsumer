using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Region
    {
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("libelle")]
        public string Label { get; set; } = string.Empty;
    }
}
