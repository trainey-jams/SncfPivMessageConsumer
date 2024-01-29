﻿using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Brand
    {
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("libelle")]
        public string Name { get; set; } = string.Empty;
    }
}
