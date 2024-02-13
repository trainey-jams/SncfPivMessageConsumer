using Newtonsoft.Json;
using System.Text.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class SituationalMessages
    {
        [JsonProperty("messagesConjoncturels")]
        public List<SituationalMessage> Messages { get; set; } = new List<SituationalMessage>();    
    }
}
