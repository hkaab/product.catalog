using System;

namespace ProductCatalog.Library.Extensions;

public static class DateTimeExtensions
{
    public static bool IsValidDate(DateTime? value)
    {
        if (value is null)
        {
            return true;
        }
        return !value.Equals(default(DateTime));
    }
}