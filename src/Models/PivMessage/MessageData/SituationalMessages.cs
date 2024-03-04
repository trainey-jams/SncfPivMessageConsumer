using Newtonsoft.Json;
using System.Text.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class SituationalMessages
    {
        [JsonProperty("messagesConjoncturels")]
        public List<SituationalMessage> Messages { get; set; } = new List<SituationalMessage>();
    }
}
