namespace ProductCatalog.Library;

public class Constants
{
    public const string WooliesXHttpClientName = "WooliesX";
    public const string IsoFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
    public static class PII
    {
        public static readonly IList<string> Attributes = new List<string>  { "firstname", "lastname"};
        public const string MaskString = "xxxx";
    }
}
