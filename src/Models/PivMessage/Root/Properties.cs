using Newtonsoft.Json;
using SncfPivMessageConsumer.Models.Enums;
using System.Text.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.Root
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
        public List<string> StopsServedByServiceNumber { get; set; } = new List<string>();

        [JsonProperty("circulation.listeArretDesserte.arret[]")]
        public List<string> ServiceStops { get; set; } = new List<string>(); //Maybe the difference between these is planned and actual?

        [JsonProperty("circulation.indicateurCourseDeReference")]
        public bool CourseOfReferenceIndicator { get; set; }

        [JsonProperty("circulation.planTransportSource")]
        public string ServiceTransportPlanSource { get; set; } = string.Empty;

        [JsonProperty("circulation.marque.Code")]
        public string BrandCode { get; set; } = string.Empty;

        [JsonProperty("circulation.evenement[]")]
        public List<ServiceEvent> ServiceEvents { get; set; } = new List<ServiceEvent>();

        public string NotificationTrigger { get; set; } = string.Empty;

        [JsonProperty("circulation.parcours.route.ligne.idLigne")]
        public string RouteLineId { get; set; } = string.Empty;

        [JsonProperty("circulation.Operator.codeOperateur")]
        public string OperatorCode { get; set; } = string.Empty;

        [JsonProperty("circulation.codeMission")]
        public string CodeMission { get; set; } = string.Empty;

        [JsonProperty("circulation.modeTransport.codeMode")]
        public TransportModeCode TransportModeCode { get; set; }

        [JsonProperty("circulation.modeTransport.typeMode")]
        public TransportModeType? TransportModeType { get; set; }

        [JsonProperty("circulation.codeTransporteurResponsable")]
        public string ResponsibleTrasporterCode { get; set; } = string.Empty;

        [JsonProperty("circulation.indicateurAdaptation")]
        public bool ServiceAdaptationIndicator { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; } = string.Empty;
    }
}
