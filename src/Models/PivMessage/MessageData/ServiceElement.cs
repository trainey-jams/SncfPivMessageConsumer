using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class ServiceElement
    {
        [JsonProperty("voiture")]
        public List<TrainCarriage> TrainCarriages { get; set; } = new List<TrainCarriage>();

        [JsonProperty("typeMateriel")]
        public string TrainType { get; set; } = string.Empty;

        [JsonProperty("serieMateriel")]
        public string TrainSeries { get; set; } = string.Empty;

        [JsonProperty("familleMateriel")]
        public string TrainFamily { get; set; } = string.Empty;

        [JsonProperty("libelleFamilleMateriel")]
        public string TrainFamilyLabel { get; set; } = string.Empty;

        [JsonProperty("codeNature")]
        public string NatureCode { get; set; } = string.Empty;

        [JsonProperty("libelleNature")]
        public string NatureLabel { get; set; } = string.Empty;

        [JsonProperty("numeroAffectation")]
        public string AssignmentNumber { get; set; } = string.Empty;

        [JsonProperty("rangElement")]
        public string ElementRank { get; set; } = string.Empty;

        [JsonProperty("nomLivree")]
        public string InsigniaName { get; set; } = string.Empty;

        [JsonProperty("imageLivree")]
        public string InsigniaImage { get; set; } = string.Empty;

        [JsonProperty("nomCommercial")]
        public string CommercialName { get; set; } = string.Empty;

        [JsonProperty("nombreVoituresCommerciales")]
        public string NumberOfCommercialCarriages { get; set; } = string.Empty;

        [JsonProperty("nombrePlacesAssises")]
        public string NumberOfSeats { get; set; } = string.Empty;
    }
}
