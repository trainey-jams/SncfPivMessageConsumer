using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class DoorList
    {
        [JsonProperty("porte")]
        public List<Door> Doors { get; set; } = new List<Door>();
    }
}
