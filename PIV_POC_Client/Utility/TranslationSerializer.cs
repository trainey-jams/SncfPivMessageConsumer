using Newtonsoft.Json;

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
