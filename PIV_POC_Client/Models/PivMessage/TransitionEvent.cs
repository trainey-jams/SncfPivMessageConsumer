using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class TransitionEvent
    {
        public AffichageIV affichageIV { get; set; } = new AffichageIV();

        [JsonProperty("composition")]
        public ServiceComposition ServiceComposition { get; set; } = new ServiceComposition();

        [JsonProperty("numeroCirculation")]
        public string TrafficNumber { get; set; } = string.Empty;

        [JsonProperty("numeroMarche")]
        public string MarketNumber { get; set; } = string.Empty;

        [JsonProperty("dateHeure")]
        public DateTime PlannedDateTime { get; set; }

        [JsonProperty("dateHeureReelle")]
        public DateTime ActualDateTime { get; set; }

        [JsonProperty("dateHeureInterne")]
        public DateTime InternalDateTime { get; set; }


        public string typeAffichage { get; set; } = string.Empty;

        public bool indicateurAdaptation { get; set; }

        public string planTransportSource { get; set; } = string.Empty;
    }
}
