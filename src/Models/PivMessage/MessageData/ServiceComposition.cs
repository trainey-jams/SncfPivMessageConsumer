using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class ServiceComposition
    {
        [JsonProperty("element")]
        public List<ServiceElement> Elements { get; set; } = new List<ServiceElement>();

        [JsonProperty("nombreElements")]
        public int? NumberOfElements { get; set; }
    }
}
