using Newtonsoft.Json;
using PIV_POC_Client.Models.Enums;
using System.Text.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Properties
    {
        [JsonProperty("circulation.numero")]
        public string ServiceNumber { get; set; } = string.Empty;

        [JsonProperty("circulation.codeCirculation")]
        public string ServiceCode { get; set; } = string.Empty;

        [JsonProperty("circulation.dateCirculation")]
        public string ServiceDate { get; set; } = string.Empty;

        [JsonProperty("nbJoursAvantDateCirculation")]
        public int DaysBeforeServiceDate { get; set; }

        [JsonProperty("circulation.listeArretDesserte.numeroCirculation[]")]
        public List<string> StopsServedByCirculationNumber { get; set; } = new List<string>();

        [JsonProperty("circulation.listeArretDesserte.arret[]")]
        public List<string> ServiceStops { get; set; } = new List<string>(); //Maybe the difference between these is planned and actual?

        [JsonProperty("circulation.indicateurCourseDeReference")]
        public bool CirculationIndicatorReference { get; set; }

        [JsonProperty("circulation.planTransportSource")]
        public string CirculationPlanTransportSource { get; set; } = string.Empty;

        [JsonProperty("circulation.marque.Code")]
        public string BrandCode { get; set; } = string.Empty;

        [JsonProperty("circulation.evenement[]")]
        public List<ServiceEvents> ServiceEvents { get; set; } = new List<ServiceEvents>();

        public string NotificationTrigger { get; set; } = string.Empty;

        [JsonProperty("circulation.parcours.route.ligne.idLigne")]
        public string RouteLineId { get; set; } = string.Empty;

        [JsonProperty("circulation.Operator.codeOperateur")]
        public string OperatorCode { get; set; } = string.Empty;

        [JsonProperty("circulation.codeMission")]
        public string CodeMission { get; set; } = string.Empty;

        [JsonProperty("circulation.modeTransport.codeMode")]
        public string TransportModeCode { get; set; } = string.Empty;

        [JsonProperty("circulation.modeTransport.typeMode")]
        public string TransportModeType { get; set; } = string.Empty;

        [JsonProperty("circulation.codeTransporteurResponsable")]
        public string ResponsibleTrasporterCode { get; set; } = string.Empty;

        [JsonProperty("circulation.indicateurAdaptation")]
        public bool CirculationIndicatorAdaptation { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; } = string.Empty;
    }
}
