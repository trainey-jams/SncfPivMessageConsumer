using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class ServiceCallingPoints
    {
        [JsonProperty("arret")]
        public List<CallingPoint> CallingPoints { get; set; } = new List<CallingPoint>();

        [JsonProperty("nombreEscales")]
        public int? NumberOfCallingPoints { get; set; }
    }
}
