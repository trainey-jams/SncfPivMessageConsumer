using Newtonsoft.Json;
using PIV_POC_Client.Models.Enums;
using System.Text.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class PivMessageData
    {
        [JsonProperty("codeCirculation")]
        public string ServiceCode { get; set; } = string.Empty;

        [JsonProperty("dateCirculation")]
        public string ServiceDate { get; set; } = string.Empty;

        [JsonProperty("calendrierCirculation")]
        public List<string> ServiceCalendar { get; set; } = new List<string>();

        [JsonProperty("statutCommercial")]
        public CommericalStatus CommericalStatus { get; set; } = new CommericalStatus();

        [JsonProperty("origine")]
        public Terminus Origin { get; set; } = new Terminus();

        [JsonProperty("destination")]
        public Terminus Destination { get; set; } = new Terminus();

        [JsonProperty("listeReservations")]
        public ListReservations ListOfReservations { get; set; } = new ListReservations();

        [JsonProperty("listeServicesABord")]
        public ServicesAboard ServicesAboard { get; set; } = new ServicesAboard();

        [JsonProperty("listeArretsDesserte")]
        public ServiceCallingPoints ServiceCallingPoints { get; set; } = new ServiceCallingPoints();

        [JsonProperty("affichageIV")]
        public DisplayableInformationVoyager DisplayableInformationVoyager { get; set; } = new DisplayableInformationVoyager();

        [JsonProperty("operateur")]
        public Operator Operator { get; set; } = new Operator();

        [JsonProperty("modeTransport")]
        public TransportMode TransportMode { get; set; } = new TransportMode();

        [JsonProperty("marque")]
        public Brand Brand { get; set; } = new Brand();

        [JsonProperty("parcours")]
        public Journey Journey { get; set; } = new Journey();

        [JsonProperty("relation")]
        public Relation Relation { get; set; } = new Relation();

        [JsonProperty("numero")]
        public string Number { get; set; } = string.Empty;

        [JsonProperty("dateDerniereModification")]
        public DateTime LastModificationDate { get; set; }

        [JsonProperty("typeEquipement")]
        public string EquipmentType { get; set; } = string.Empty;

        [JsonProperty("codeMission")]
        public string CodeMission { get; set; } = string.Empty;

        [JsonProperty("natureTrain")]
        public string NatureTrain { get; set; } = string.Empty;


        [JsonProperty("planTransportSource")]
        public TransportPlanSource TransportPlanSource { get; set; }

        [JsonProperty("indicateurAdaptation")] // indicates whether traffic was impacted by PTP or not.
        public bool? AdaptationIndicator { get; set; }

        [JsonProperty("indicateurCourseDeReference")]
        public bool? CourseOfReferenceIndicator { get; set; }

        public string codeTransporteurResponsable { get; set; } = string.Empty;

        public string idPlanTransportPerturbe { get; set; } = string.Empty;
    }
}
