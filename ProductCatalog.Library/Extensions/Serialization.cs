using Newtonsoft.Json;

namespace ProductCatalog.Library.Extensions;

public static class Serialization
{
    public static T? FromJson<T>(this string json) where T : class
    {
        if (json == null)
        {
            return default;
        }

        return JsonConvert.DeserializeObject<T>(json);
    }
}
