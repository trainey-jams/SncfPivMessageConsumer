using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class ServiceComposition
    {
        [JsonProperty("element")]
        public List<Element> Elements { get; set; } = new List<Element>();

        [JsonProperty("nombreElements")]
        public int NumberOfElements { get; set; }
    }
}
