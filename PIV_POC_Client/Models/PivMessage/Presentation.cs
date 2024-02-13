using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Presentation
    {
        [JsonProperty("codeCouleur")]
        public string ColourCode { get; set; } = string.Empty;
    }
}
