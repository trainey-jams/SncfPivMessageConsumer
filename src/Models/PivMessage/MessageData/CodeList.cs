using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class CodeList
    {
        [JsonProperty("valeur")]
        public List<ValueItem> Values { get; set; } = new List<ValueItem>();

        [JsonProperty("typeDefaut")]
        public string DefaultType { get; set; } = string.Empty;
    }
}
