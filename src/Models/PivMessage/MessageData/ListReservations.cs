using Newtonsoft.Json;

namespace PIV_POC_Client.Models.PivMessage.MessageData
{
    public class ListReservations
    {
        [JsonProperty("reservation")]
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
