using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Relation
    {
        [JsonProperty("codeRelation")]
        public string RelationCode { get; set; } = string.Empty;
    }
}
