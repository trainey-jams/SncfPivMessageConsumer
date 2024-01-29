using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage
{
    public class TrainCarriage
    {
        [JsonProperty("listePortes")]
        public DoorList DoorList { get; set; } = new DoorList();

        public string categorie { get; set; } = string.Empty;

        [JsonProperty("longueur")]
        public string Length { get; set; } = string.Empty;
    }
}
