using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class CoordinateList
    {
        [JsonProperty("coordonnees")]
        public List<Coordinates> Coordinates { get; set; } = new List<Coordinates>();
    }
}
