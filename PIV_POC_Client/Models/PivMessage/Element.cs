using Newtonsoft.Json;
using System.Text.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Element
    {
        [JsonProperty("voiture")]
        public List<TrainCarriage> TrainCarriages { get; set; } = new List<TrainCarriage>();

        public string typeMateriel { get; set; } = string.Empty;

        public string serieMateriel { get; set; } = string.Empty;

        public string familleMateriel { get; set; } = string.Empty;

        public string libelleFamilleMateriel { get; set; } = string.Empty;

        public string codeNature { get; set; } = string.Empty;

        public string libelleNature { get; set; } = string.Empty;

        public string numeroAffectation { get; set; } = string.Empty;

        public string rangElement { get; set; } = string.Empty;

        public string nomLivree { get; set; } = string.Empty;

        public string imageLivree { get; set; } = string.Empty;

        public string nomCommercial { get; set; } = string.Empty;

        [JsonProperty("nombreVoituresCommerciales")]
        public string NumberOfCommercialCarriages { get; set; } = string.Empty;

        [JsonProperty("nombrePlacesAssises")]
        public string NumberOfSeats { get; set; } = string.Empty;
    }
}
