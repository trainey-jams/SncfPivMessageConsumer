using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class CallingPoint
    {
        [JsonProperty("arrivee")]
        public TransitionEvent Arrival { get; set; } = new TransitionEvent();

        [JsonProperty("depart")]
        public TransitionEvent Departure { get; set; } = new TransitionEvent();

        [JsonProperty("emplacement")]
        public Location Location { get; set; } = new Location();

        [JsonProperty("voie")]
        public Track Track { get; set; } = new Track();

        public int rang { get; set; }

        public int rangInterne { get; set; }

        [JsonProperty("quai")]
        public Platform Platform { get; set; } = new Platform();

        [JsonProperty("dureeStationnement")]
        public int? PlannedStopDurationMins { get; set; }

        [JsonProperty("dureeStationnementReelle")]
        public int? ActualStopDurationMins { get; set; }

        [JsonProperty("indicateurMonteeInterdite")]
        public bool? ProhibitedClimbIndicator { get; set; }

        [JsonProperty("indicateurDescenteInterdite")]
        public bool? ProhibitedDescentIndicator { get; set; }
    }
}
