using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class CoordinateList
    {
        [JsonProperty("coordonnees")]
        public List<Coordinates> Coordinates { get; set; } = new List<Coordinates>();
    }
}
