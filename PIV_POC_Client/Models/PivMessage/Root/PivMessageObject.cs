using Newtonsoft.Json;
using PIV_POC_Client.Models.PivMessage.MessageData;
using System.Text.Json;

namespace PIV_POC_Client.Models.PivMessage.Root
{
    public class PivMessageObject
    {
        public string ObjectId { get; set; } = string.Empty;

        [JsonProperty("Object")]
        public PivMessageData MessageData { get; set; } = new PivMessageData();

        public Properties Properties { get; set; } = new Properties();
    }
}
