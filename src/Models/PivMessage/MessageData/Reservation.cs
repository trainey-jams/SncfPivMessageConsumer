using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class Reservation
    {
        [JsonProperty("codeClasse")]
        public string ClassCode { get; set; } = string.Empty;

        [JsonProperty("libelleClasse")]
        public string ClassLabel { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}
