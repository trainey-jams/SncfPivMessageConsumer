﻿using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Coordinates
    {
        [JsonProperty("coordXLng")]
        public double? XCoordinateLongitude { get; set; }

        [JsonProperty("coordYLat")]
        public double? YCoordinateLatitude { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}
