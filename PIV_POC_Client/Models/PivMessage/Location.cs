using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Location
    {
        public string code { get; set; }

        public string type { get; set; }

        public ListeCoordonnees listeCoordonnees { get; set; }

        public Commune commune { get; set; }

        public Department departement { get; set; }

        public Region region { get; set; }

        public Country pays { get; set; }

        public StopZoneInformation infosZoneArret { get; set; }

        [JsonProperty("libelle")]
        public string Label { get; set; }
    }
}
