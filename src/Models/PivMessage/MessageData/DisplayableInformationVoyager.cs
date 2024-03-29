﻿using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class DisplayableInformationVoyager
    {
        [JsonProperty("indicateurAffichableTableauxGare")]
        public bool? StationTableDisplayable { get; set; }

        [JsonProperty("indicateurAffichableItineraire")]
        public bool? RouteDisplayable { get; set; }

        [JsonProperty("indicateurAffichableTrajetEnregistre")]
        public bool? SavedTripDisplayable { get; set; }

        [JsonProperty("indicateurAffichableDetailTrain")]
        public bool? TrainDetailsDisplayable { get; set; }
    }
}
