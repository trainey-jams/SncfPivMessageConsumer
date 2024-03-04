using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class CoordinateList
    {
        [JsonProperty("coordonnees")]
        public List<Coordinates> Coordinates { get; set; } = new List<Coordinates>();
    }
}
