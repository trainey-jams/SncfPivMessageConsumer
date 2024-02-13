using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Terminus
    {
        [JsonProperty("listeCoordonnees")]
        public CoordinateList CoordinateList { get; set; } = new CoordinateList();

        [JsonProperty("commune")]
        public Commune Commune { get; set; } = new Commune();

        [JsonProperty("departement")]
        public Department Departement { get; set; } = new Department();

        [JsonProperty("region")]
        public Region Region { get; set; } = new Region();

        [JsonProperty("pays")]
        public Country Country { get; set; } = new Country();

        [JsonProperty("listeMessagesConjoncturels")]
        public SituationalMessages SituationalMessages { get; set; } = new SituationalMessages();

        [JsonProperty("infosZoneArret")]
        public StopZoneInformation StopZoneInformation { get; set; } = new StopZoneInformation();

        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("libelle")]
        public string Label { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}
