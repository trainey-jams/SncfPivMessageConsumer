using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Route
    {
        [JsonProperty("ligne")]
        public Line Line { get; set; } = new Line();
    }
}
