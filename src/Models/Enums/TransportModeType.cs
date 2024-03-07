using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.Enums;

public enum TransportModeType
{
    [JsonProperty("FERRE")] FERRE,

    [JsonProperty("ROUTIER")] ROUTIER
}