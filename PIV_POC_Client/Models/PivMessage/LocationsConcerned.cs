using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class LocationsConcerned
    {
        [JsonProperty("emplacement")]
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
