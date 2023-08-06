using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

public class IgnoreJsonIgnoreResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        var jsonIgnoreAttribute = member.GetCustomAttribute<JsonIgnoreAttribute>();
        if (jsonIgnoreAttribute != null && property.Ignored)
        {
            property.Ignored = true;
        }

        return property;
    }
}