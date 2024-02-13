using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class TransitionEvent
    {
        [JsonProperty("affichageIV")]
        public DisplayableInformationVoyager DisplayableInformationVoyager { get; set; } = new DisplayableInformationVoyager();

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

        [JsonProperty("typeAffichage")]
        public string DisplayType { get; set; } = string.Empty;

        [JsonProperty("indicateurAdaptation")]
        public bool AdaptationIndicator { get; set; }

        [JsonProperty("planTransportSource")]
        public string TransportPlanSource { get; set; } = string.Empty;
    }
}
