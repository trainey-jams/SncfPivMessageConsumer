using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class LocationsConcerned
    {
        [JsonProperty("emplacement")]
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
