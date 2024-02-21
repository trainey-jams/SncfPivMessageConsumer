using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class ServiceElement
    {
        [JsonProperty("voiture")]
        public List<TrainCarriage> TrainCarriages { get; set; } = new List<TrainCarriage>();

        [JsonProperty("typeMateriel")]
        public string MaterialType { get; set; } = string.Empty;

        [JsonProperty("serieMateriel")]
        public string MaterialSeries { get; set; } = string.Empty;

        [JsonProperty("familleMateriel")]
        public string MaterialFamily { get; set; } = string.Empty;

        [JsonProperty("libelleFamilleMateriel")]
        public string MaterialFamilyLabel { get; set; } = string.Empty;

        [JsonProperty("codeNature")]
        public string NatureCode { get; set; } = string.Empty;

        [JsonProperty("libelleNature")]
        public string NatureLabel { get; set; } = string.Empty;

        [JsonProperty("numeroAffectation")]
        public string AssignmentNumber { get; set; } = string.Empty;

        [JsonProperty("rangElement")]
        public string ElementRank { get; set; } = string.Empty;

        [JsonProperty("nomLivree")]
        public string nomLivree { get; set; } = string.Empty;

        [JsonProperty("imageLivree")]
        public string imageLivree { get; set; } = string.Empty;

        [JsonProperty("CommercialName")]
        public string nomCommercial { get; set; } = string.Empty;

        [JsonProperty("nombreVoituresCommerciales")]
        public string NumberOfCommercialCarriages { get; set; } = string.Empty;

        [JsonProperty("nombrePlacesAssises")]
        public string NumberOfSeats { get; set; } = string.Empty;
    }
}
