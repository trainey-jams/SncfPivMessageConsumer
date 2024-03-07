using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.Enums;

public enum TransportModSubCode
{
    [JsonProperty("highSpeedRail")] HighSpeedRail,

    [JsonProperty("suburbanRailway")] SuburbanRailway,

    [JsonProperty("international")] International,

    [JsonProperty("interregionalRail")] InterregionalRail,

    [JsonProperty("longDistance")] LongDistance,

    [JsonProperty("railShuttle")] RailShuttle,

    [JsonProperty("regionalRail")] RegionalRail,

    [JsonProperty("specialTrain")] SpecialTrain,

    [JsonProperty("touristRailway")] TouristRailway,

    [JsonProperty("crossCountryRail")] CrossCountryRail,

    [JsonProperty("highServicelevelCoach")]
    HighServicelevelCoach,

    [JsonProperty("railReplacementCoach")] RailReplacementCoach,

    [JsonProperty("commuterCoach")] CommuterCoach,

    [JsonProperty("regionalCoach")] RegionalCoach,

    [JsonProperty("specialCoach")] SpecialCoach,

    [JsonProperty("touristCoach")] TouristCoach,

    [JsonProperty("specialCoach")] OnDemandCoach,

    [JsonProperty("shuttleCoach")] ShuttleCoach,

    [JsonProperty("tramTrain")] TramTrain
}