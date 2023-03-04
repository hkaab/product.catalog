using Newtonsoft.Json;

namespace ProductCatalog.Models;

public class ShopperHistory
{
    [JsonProperty] 
    public string CustomerId { get; set; }
    [JsonProperty]
    public IList<Product> Products { get; private set; }
}