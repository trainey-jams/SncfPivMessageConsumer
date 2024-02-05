using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class ListeCodes
    {
        [JsonProperty("valeur")]
        public List<Valeur> ListOfValues { get; set; } = new List<Valeur>();

        [JsonProperty("typeDefaut")]
        public string DefaultType { get; set; } = string.Empty;
    }
}
