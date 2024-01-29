using Newtonsoft.Json;
using PIV_POC_Client.Models.Enums;

namespace PIV_POC_Client.Models.PivMessage
{
    public class CommericalStatus
    {
        [JsonProperty("codeStatut")]
        public CommericalStatusCode? StatusCode { get; set; }

        [JsonProperty("libelleStatut")]
        public string StatusLabel { get; set; }
    }
}
