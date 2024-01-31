using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class ServicesAboard
    {
        [JsonProperty("serviceABord")]
        public List<ServiceAboard> Services { get; set; } = new List<ServiceAboard>();
    }
}
