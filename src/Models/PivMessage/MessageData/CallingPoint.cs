using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
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

        [JsonProperty("rang")]
        public int? Rank { get; set; } // If train goes from station A to station C, A -> B -> C Then first station A will have rank 1, the second one B will have rank 2 etc.
                                       // There might be a more suitable name for it.
        
        [JsonProperty("rangInterne")]
        public int? InternalRank { get; set; }

        [JsonProperty("quai")]
        public Platform Platform { get; set; } = new Platform();

        [JsonProperty("listeMessagesConjoncturels")]
        public SituationalMessages SituationalMessages { get; set; } = new SituationalMessages();

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
