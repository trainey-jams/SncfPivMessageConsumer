using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.Enums;

public enum TransportModeCode
{
    [JsonProperty("rail")] Rail,

    [JsonProperty("coach")] Coach,

    [JsonProperty("bus")] Bus,

    [JsonProperty("tram")] Tram,
}