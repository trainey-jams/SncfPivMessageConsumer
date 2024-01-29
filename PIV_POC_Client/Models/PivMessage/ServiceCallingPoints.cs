using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class ServiceCallingPoints
    {
        [JsonProperty("arret")]
        public List<CallingPoint> CallingPoints { get; set; }

        [JsonProperty("nombreEscales")]
        public int NumberOfCallingPoints { get; set; }
    }
}
