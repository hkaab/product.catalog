using Newtonsoft.Json.Linq;
using static ProductCatalog.Library.Constants;

namespace ProductCatalog.Library.Extensions;

public static class JObjectExtensions
{
    public static void MaskPersonalInformation(this JObject obj)
    {
        Iterate(obj, o =>
        {
            foreach (var kvp in o)
            {
                if (PII.Attributes.Contains(kvp.Key.ToLower()))
                    o[kvp.Key] = PII.MaskString;
            }
        });
    }

    private static void Iterate(JToken node, Action<JObject> action)
    {
        if (node.Type == JTokenType.Object)
        {
            action((JObject)node);

            foreach (JProperty child in node.Children<JProperty>())
            {
                Iterate(child.Value, action);
            }
        }
        else if (node.Type == JTokenType.Array)
        {
            foreach (JToken child in node.Children())
            {
                Iterate(child, action);
            }
        }
    }
}