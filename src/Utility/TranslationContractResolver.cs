using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PIV_POC_Client.Utility
{
    public class TranslationContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);

            // Now inspect each property and replace jsonproperty name with class property name.
            foreach (JsonProperty prop in list)
            {
                prop.PropertyName = prop.UnderlyingName;
            }

            return list;
        }
    }
}
