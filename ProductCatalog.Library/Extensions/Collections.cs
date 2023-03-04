using System.Reflection;
using ProductCatalog.Library.Extensions;
using ProductCatalog.Models.Enums;

namespace ProductCatalog.Library.Extensions;

public static class Collections
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> items, string property )
    {
        if (!items.Any() || string.IsNullOrEmpty(property))
            return items;

        var propertyInfo = items?.First()?.GetType().GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        return propertyInfo != null ? items.OrderBy(e => propertyInfo.GetValue(e, null)) : items;
    }
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> items, string property, SortOptions? sortOptions)
    {
        if (!items.Any() || string.IsNullOrEmpty(property))
            return items;

        var propertyInfo = items?.First()?.GetType().GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (sortOptions == SortOptions.Ascending)
            return propertyInfo != null ? items.OrderBy(e => propertyInfo.GetValue(e, null)) : items;
        return propertyInfo != null ? items.OrderByDescending(e => propertyInfo.GetValue(e, null)) : items;
    }
}
