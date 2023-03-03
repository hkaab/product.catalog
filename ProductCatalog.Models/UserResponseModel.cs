using Newtonsoft.Json;

namespace ProductCatalog.Models;

public class UserResponseModel
{
    [JsonProperty]
    public string? Token { get; set; }

    [JsonProperty]
    public string? Name { get; set; }
}
