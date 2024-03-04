using Newtonsoft.Json;
using SncfPivMessageConsumer.Models.Enums;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class CommericalStatus
    {
        [JsonProperty("codeStatut")]
        public CommericalStatusCode? StatusCode { get; set; }

        [JsonProperty("libelleStatut")]
        public string StatusLabel { get; set; } = string.Empty;
    }
}
