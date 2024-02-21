using Newtonsoft.Json;

namespace PIV_POC_Client.Models.Enums
{
    public enum TransportModeCode
    {
        [JsonProperty("rail")]
        Rail,

        [JsonProperty("coach")]
        Coach,

        [JsonProperty("bus")]
        Bus,

        [JsonProperty("tram")]
        Tram,
    }
}
