﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PIV_POC_Client.Models.Enums
{
    public enum ServiceEvents
    {
        CREATION,
        SUPPRESSION,
        RETARD,

        [JsonProperty("MODIFICATION DESSERTE AJOUTEE")]
        [Display(Name = "MODIFICATION DESSERTE AJOUTEE")]
        MODIFICATION_DESSERTE_AJOUTEE,

        [JsonProperty("MODIFICATION DESSERTE SUPPRIMEE")]
        [Display(Name = "MODIFICATION DESSERTE SUPPRIMEE")]
        MODIFICATION_DESSERTE_SUPPRIMEE,

        [JsonProperty("MODIFICATION PROLONGATION")]
        [Display(Name = "MODIFICATION PROLONGATION")]
        MODIFICATION_PROLONGATION,

        [JsonProperty("MODIFICATION LIMITATION")]
        [Display(Name = "MODIFICATION LIMITATION")]
        MODIFICATION_LIMITATION,

        [JsonProperty("MODIFICATION DETOURNEMENT")]
        [Display(Name = "MODIFICATION DETOURNEMENT")]
        MODIFICATION_DETOURNEMENT,
    }
}
