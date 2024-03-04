using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Journey
    {
        [JsonProperty("route")]
        public Route Route { get; set; } = new Route();
    }
}
