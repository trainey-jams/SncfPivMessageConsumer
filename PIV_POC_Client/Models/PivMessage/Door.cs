using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Door
    {
        [JsonProperty("position")]
        public string Position { get; set; } = string.Empty;
    }
}
