using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class TrainCarriage
    {
        [JsonProperty("listePortes")]
        public DoorList DoorList { get; set; } = new DoorList();

        [JsonProperty("categorie")]
        public string Class { get; set; } = string.Empty;

        [JsonProperty("longueur")]
        public string Length { get; set; } = string.Empty;
    }
}
