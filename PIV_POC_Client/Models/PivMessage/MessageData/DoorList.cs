using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class DoorList
    {
        [JsonProperty("porte")]
        public List<Door> Doors { get; set; } = new List<Door>();
    }
}
