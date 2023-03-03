namespace ProductCatalog.Library.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal? TrimTrailingZeros(this decimal? value)
        {
            if (!value.HasValue)
            {
                return value;
            }
            return value / 1.0000000000m;
        }
    }
}