using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class StopZoneInformation
    {
        [JsonProperty("listeCodes")]
        public CodeList CodeList { get; set; } = new CodeList();

        [JsonProperty("indicateurSNCF")]
        public bool SncfIndicator { get; set; }

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
