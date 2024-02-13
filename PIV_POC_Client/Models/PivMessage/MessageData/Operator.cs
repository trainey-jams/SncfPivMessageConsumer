using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class Operator
    {
        [JsonProperty("codeOperateur")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("libelleOperateur")]
        public string Name { get; set; } = string.Empty;
    }
}
