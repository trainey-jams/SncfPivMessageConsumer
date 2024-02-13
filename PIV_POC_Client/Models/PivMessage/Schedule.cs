using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Schedule
    {
        [JsonProperty("dateDebut")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("dateFin")]
        public DateTime? EndDate { get; set; }
    }
}
