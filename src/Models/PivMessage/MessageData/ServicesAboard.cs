using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class ServicesAboard
    {
        [JsonProperty("serviceABord")]
        public List<ServiceAboard> Services { get; set; } = new List<ServiceAboard>();
    }
}
