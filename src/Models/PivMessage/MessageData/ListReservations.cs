using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.PivMessage.MessageData
{
    public class ListReservations
    {
        [JsonProperty("reservation")]
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
