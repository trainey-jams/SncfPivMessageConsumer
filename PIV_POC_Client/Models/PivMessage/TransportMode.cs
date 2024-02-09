using Newtonsoft.Json;
using PIV_POC_Client.Models.Enums;

namespace PIV_POC_Client.Models.PivMessage
{
    public class TransportMode
    {
        [JsonProperty("codeMode")]
        public TransportModeCode? Code { get; set; }

        [JsonProperty("libelleMode")]
        public string Label { get; set; } = string.Empty;

        [JsonProperty("codeSousMode")]
        public TransportModSubCode? SubCode { get; set; }

        [JsonProperty("libelleSousMode")]
        public string SubLabel { get; set; } = string.Empty;

        [JsonProperty("typeMode")]
        public TransportModeType? Type { get; set; }
    }
}
