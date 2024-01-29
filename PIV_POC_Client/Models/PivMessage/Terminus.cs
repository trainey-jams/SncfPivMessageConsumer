using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Terminus
    {
        public ListeCoordonnees listeCoordonnees { get; set; }

        public Commune commune { get; set; }

        public Department departement { get; set; }

        public Region region { get; set; }

        [JsonProperty("pays")]
        public Country Country { get; set; } = new Country();

        [JsonProperty("infosZoneArret")]
        public StopZoneInformation StopZoneInformation { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("libelle")]
        public string Label { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}
