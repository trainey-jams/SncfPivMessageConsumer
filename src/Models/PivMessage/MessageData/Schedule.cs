using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Schedule
    {
        [JsonProperty("dateDebut")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("dateFin")]
        public DateTime? EndDate { get; set; }
    }
}
