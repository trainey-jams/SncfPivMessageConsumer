using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIV_POC_Client.Utility
{
    public static class TranslationSerializer
    {
        public static string Serialize(object obj, bool useLongNames)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            if (useLongNames)
            {
                settings.ContractResolver = new TranslationContractResolver();
            }

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
