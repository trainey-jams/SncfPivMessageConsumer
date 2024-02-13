using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class OtherModesOfTransport
    {
        [JsonProperty("autreModeTransport")]
        public List<TransportMode> TransportModes { get; set; } = new List<TransportMode>();
    }
}
