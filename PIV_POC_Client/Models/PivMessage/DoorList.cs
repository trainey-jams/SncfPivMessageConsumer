using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class DoorList
    {
        [JsonProperty("porte")]
        public List<Door> Doors { get; set; }
    }
}
