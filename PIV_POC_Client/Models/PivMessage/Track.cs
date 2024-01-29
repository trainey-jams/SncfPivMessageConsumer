using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Track
    {
        [JsonProperty("numero")]
        public string Number { get; set; } = string.Empty;

        [JsonProperty("numeroQuai")]
        public string PlatformNumber { get; set; } = string.Empty;
    }
}
