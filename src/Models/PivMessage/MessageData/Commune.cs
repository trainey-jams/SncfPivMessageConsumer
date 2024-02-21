using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class Commune
    {
        [JsonProperty("codePostal")]
        public string PostalCode { get; set; } = string.Empty;

        //INSE is the French national institute of stats/economics
        [JsonProperty("codeINSEE")]
        public string InseeCode { get; set; } = string.Empty;

        [JsonProperty("libelle")]
        public string Label { get; set; } = string.Empty;
    }
}
