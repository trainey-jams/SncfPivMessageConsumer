using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class ServiceComposition
    {
        [JsonProperty("element")]
        public List<ServiceElement> Elements { get; set; } = new List<ServiceElement>();

        [JsonProperty("nombreElements")]
        public int? NumberOfElements { get; set; }
    }
}
