using Newtonsoft.Json;

namespace PIV_POC_Client.Models.Enums
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
