using Newtonsoft.Json;
using SncfPivMessageConsumer.Models.Enums;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
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
