using Microsoft.AspNetCore.Builder;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProductCatalog.Library.Attributes;
public class DateFormatConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        reader.TryGetDateTime(out var value);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString(Constants.IsoFormat));
    }
}