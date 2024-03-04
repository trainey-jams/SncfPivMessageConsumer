using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class Line
    {
        [JsonProperty("listeAutresModesTransport")]
        public OtherModesOfTransport OtherModesOfTransport { get; set; } = new OtherModesOfTransport();

        [JsonProperty("idLigne")]
        public string LineId { get; set; } = string.Empty;

        [JsonProperty("libelleLigne")]
        public string LineLabel { get; set; } = string.Empty;

        [JsonProperty("modeTransport")]
        public TransportMode TransportMode { get; set; } = new TransportMode();

        [JsonProperty("operateur")]
        public Operator Operator { get; set; } = new Operator();

        public Presentation Presentation { get; set; } = new Presentation();

        [JsonProperty("marque")]
        public Brand Brand { get; set; } = new Brand();

        [JsonProperty("plageTemporelleApplication")]
        public Schedule ApplicableSchedule { get; set; } = new Schedule();

        [JsonProperty("plageTemporellePublication")]
        public Schedule PublishedSchedule { get; set; } = new Schedule();
    }
}
