using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Location
    {
        public string code { get; set; } = string.Empty;

        public string type { get; set; } = string.Empty;

        public ListeCoordonnees listeCoordonnees { get; set; } = new ListeCoordonnees();

        public Commune commune { get; set; } = new Commune();

        public Department departement { get; set; } = new Department();

        public Region region { get; set; } = new Region();

        public Country pays { get; set; } = new Country();

        public StopZoneInformation infosZoneArret { get; set; } = new StopZoneInformation();

        [JsonProperty("libelle")]
        public string Label { get; set; } = string.Empty;
    }
}
