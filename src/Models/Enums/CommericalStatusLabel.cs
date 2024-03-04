using Newtonsoft.Json;

namespace SncfPivMessageConsumer.Models.Enums
{
    public enum CommericalStatusLabel
    {
        [JsonProperty("Complet")]
        Complete,
        
        [JsonProperty("Fermé à la vente - pas d'horaires affichés")]
        ClosedForSaleNoHoursPosted,
        
        [JsonProperty("Bus de substitution")]
        SubstituteBus,

        [JsonProperty("Ouvert à la vente")]
        OpenForSale,
        
        [JsonProperty("Horaires à confirmer")]
        TimesToBeConfirmed,

        [JsonProperty("Suspendu à la vente")]
        SuspendedForSale,
        
        [JsonProperty("Supprimé")]
        Deleted
    }
}
