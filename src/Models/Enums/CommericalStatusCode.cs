using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.Enums
{
   public enum CommericalStatusCode
    {
        C,
        F,
        H,
        O,
        OHC,
        [JsonProperty("P/S")]
        PS,
        RF,
        S
    }
}
