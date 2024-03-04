using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class ServiceCallingPoints
    {
        [JsonProperty("arret")]
        public List<CallingPoint> CallingPoints { get; set; } = new List<CallingPoint>();

        [JsonProperty("nombreEscales")]
        public int? NumberOfCallingPoints { get; set; }
    }
}
