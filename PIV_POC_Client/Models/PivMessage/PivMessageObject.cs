using Newtonsoft.Json;
using System.Text.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class PivMessageObject
    {
        public string ObjectId { get; set; } = string.Empty;

        [JsonProperty("Object")]
        public MessageData MessageData { get; set; } = new MessageData();

        public Properties Properties { get; set; } = new Properties();

        public string Operation { get; set; } = string.Empty;
    }
}
