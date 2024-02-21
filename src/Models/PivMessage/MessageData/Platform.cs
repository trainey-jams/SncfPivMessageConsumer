using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class Platform
    {
        [JsonProperty("numero")]
        public string Number { get; set; } = string.Empty;
    }
}
