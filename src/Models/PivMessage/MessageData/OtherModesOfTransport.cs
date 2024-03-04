using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class OtherModesOfTransport
    {
        [JsonProperty("autreModeTransport")]
        public List<TransportMode> TransportModes { get; set; } = new List<TransportMode>();
    }
}
