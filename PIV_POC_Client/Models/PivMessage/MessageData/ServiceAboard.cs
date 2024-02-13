using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class ServiceAboard
    {
        [JsonProperty("listeEmplacementsConcernes")]
        public LocationsConcerned LocationsConcerned { get; set; } = new LocationsConcerned();

        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("libelle")]
        public string Label { get; set; } = string.Empty;

        [JsonProperty("nom")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("applicable")]
        public bool Applicable { get; set; }
    }
}
