using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class Journey
    {
        [JsonProperty("route")]
        public Route Route { get; set; } = new Route();
    }
}
