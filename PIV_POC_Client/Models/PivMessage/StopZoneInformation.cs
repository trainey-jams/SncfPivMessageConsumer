using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class StopZoneInformation
    {
        public ListeCodes listeCodes { get; set; } = new ListeCodes();

        public bool indicateurSNCF { get; set; }

        [JsonProperty("indicateurMultimodal")]
        public bool MultiModalIndictor { get; set; }

        [JsonProperty("indicateurQuaisVoies")]
        public bool PlatformTrackIndicator { get; set; }

        [JsonProperty("indicateurTableauDeparts")]
        public bool DepartureBoardIndicator { get; set; }

        [JsonProperty("indicateurTableauArrivees")]
        public bool ArrivalBoardIndictor { get; set; }
    }
}
