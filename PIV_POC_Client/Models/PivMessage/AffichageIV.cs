using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class AffichageIV
    {
        [JsonProperty("indicateurAffichableTableauxGare")]
        public bool indicateurAffichableTableauxGare { get; set; }

        [JsonProperty("indicateurAffichableItineraire")]
        public bool DisplayableRouteIndicator { get; set; }

        [JsonProperty("indicateurAffichableTrajetEnregistre")]
        public bool indicateurAffichableTrajetEnregistre { get; set; }

        [JsonProperty("indicateurAffichableDetailTrain")]
        public bool indicateurAffichableDetailTrain { get; set; }
    }
}
