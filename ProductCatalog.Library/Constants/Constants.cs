namespace ProductCatalog.Library;

public static class Constants
{
    public const string IsoFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
    public static class PII
    {
        public static readonly IList<string> Attributes = new List<string>  { "firstname", "lastname"};
        public const string MaskString = "xxxx";
    }
    public static class EndpointsConstants
    {
        public const string Health = "health";
        public const string TrolleyCalculator = "trolleyCalculator";
        public const string ShopperHistory = "shopperHistory";
        public const string Products = "products";
    }
}


