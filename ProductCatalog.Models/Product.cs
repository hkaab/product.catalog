using Newtonsoft.Json;

namespace ProductCatalog.Models;

public class Product 
{
    [JsonProperty] 
    public string Name { get; set; }
    [JsonProperty] 
    public double Price { get; set; }
    [JsonProperty] 
    public double Quantity { get; set; }

}