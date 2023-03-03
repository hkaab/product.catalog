using System.Text.Json.Serialization;

namespace ProductCatalog.Models.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ErrorPriority
{
    HIGH,
    MEDIUM,
    LOW
}