using Common.Core.Abstractions.Serializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Common.Infrastructure.Services.Serializer;

public class NewtonSoftService : ISerializerService
{
    /// <inheritdoc />
    public T Deserialize<T>(string text)
    {
        return JsonConvert.DeserializeObject<T>(text)!;
    }

    /// <inheritdoc />
    public string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Converters = [
                new StringEnumConverter(new CamelCaseNamingStrategy())
            ]
        });
    }

    /// <inheritdoc />
    public string Serialize<T>(T obj, Type type)
    {
        return JsonConvert.SerializeObject(obj, type, new JsonSerializerSettings());
    }
}