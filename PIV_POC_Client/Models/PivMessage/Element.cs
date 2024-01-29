using Newtonsoft.Json;
using System.Text.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Element
    {
        [JsonProperty("voiture")]
        public List<TrainCarriage> TrainCarriages { get; set; } = new List<TrainCarriage>();

        public string typeMateriel { get; set; }

        public string serieMateriel { get; set; }

        public string familleMateriel { get; set; }

        public string libelleFamilleMateriel { get; set; }

        public string codeNature { get; set; }

        public string libelleNature { get; set; }

        public string numeroAffectation { get; set; }

        public string rangElement { get; set; }

        public string nomLivree { get; set; }

        public string imageLivree { get; set; }

        public string nomCommercial { get; set; }

        [JsonProperty("nombreVoituresCommerciales")]
        public string NumberOfCommercialCarriages { get; set; } = string.Empty;

        [JsonProperty("nombrePlacesAssises")]
        public string NumberOfSeats { get; set; } = string.Empty;
    }
}
