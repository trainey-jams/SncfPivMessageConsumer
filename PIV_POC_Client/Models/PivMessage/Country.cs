using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class Country
    {
        [JsonProperty("code")]
        public string CountryCode { get; set; } = string.Empty;

        [JsonProperty("libelle")]
        public string CountryName { get; set; } = string.Empty;
    }
}
