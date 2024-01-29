using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Line
    {
        [JsonProperty("listeAutresModesTransport")]
        public OtherModesOfTransport OtherModesOfTransport { get; set; } = new OtherModesOfTransport();

        [JsonProperty("idLigne")]
        public string LineId { get; set; } = string.Empty;

        [JsonProperty("libelleLigne")]
        public string LineLabel { get; set; } = string.Empty;
    }
}
