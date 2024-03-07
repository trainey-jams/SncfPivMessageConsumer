using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SncfPivMessageConsumer.Mappers;

public class PivContractResolver : DefaultContractResolver
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