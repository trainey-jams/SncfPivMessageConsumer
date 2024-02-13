using Newtonsoft.Json;
using System.Text.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class SituationalMessage
    {
        [JsonProperty("value")]
        public string MessageValue {  get; set; } = string.Empty;

        [JsonProperty("typeObjet")]
        public string ObjectType { get; set; } = string.Empty;

        [JsonProperty("typeSource")]
        public string SourceType { get; set; } = string.Empty;

        [JsonProperty("libelleTypeMessage")]
        public string MessageType { get; set; } = string.Empty;

        [JsonProperty("codeCategorie")]
        public string CategoryCode { get; set; } = string.Empty;

        [JsonProperty("libelleCategorie")]
        public string CategoryLabel { get; set; } = string.Empty;

        [JsonProperty("codeMessage")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("contenuFormatSource")]
        public string ContentFormatSource { get; set; } = string.Empty;

        [JsonProperty("dateMAJ")]
        public string DateMj { get; set; } = string.Empty;

        [JsonProperty("dateHeureDebut")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("dateHeureFin")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("dateHeureDebutPublication")]
        public DateTime? PublicationStartDate { get; set; }

        [JsonProperty("dateHeureFinPublication")]
        public DateTime? PublicationEndDate { get; set; }

        [JsonProperty("familleMedias")]
        public string MediaFamily { get; set; } = string.Empty;
    }
}
