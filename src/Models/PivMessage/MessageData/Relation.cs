using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Relation
    {
        [JsonProperty("codeRelation")]
        public string RelationCode { get; set; } = string.Empty;
    }
}
