using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class OtherModesOfTransport
    {
        [JsonProperty("autreModeTransport")]
        public List<ModeOfTransport> ModesOfTransport { get; set; } = new List<ModeOfTransport>();
    }
}
