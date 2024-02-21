using Newtonsoft.Json;

namespace PIV_POC_Client.Models.Enums
{
    public enum TransportModeType
    {
        [JsonProperty("FERRE")]
        FERRE,

        [JsonProperty("ROUTIER")]
        ROUTIER
    }
}
