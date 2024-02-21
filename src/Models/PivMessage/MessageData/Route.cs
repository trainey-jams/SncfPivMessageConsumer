using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class Route
    {
        [JsonProperty("ligne")]
        public Line Line { get; set; } = new Line();
    }
}
