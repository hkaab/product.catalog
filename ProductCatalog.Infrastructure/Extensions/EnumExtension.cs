using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ProductCatalog.Infrastructure.Extensions;

public static class EnumExtension
{
    public static TAttribute GetAttribute<TAttribute>(this Enum @enum)
        where TAttribute : Attribute
    {
        return @enum.GetType()
            .GetMember(@enum.ToString())
            .First()
            .GetCustomAttribute<TAttribute>();
    }

    /// <summary>
    /// Returns the display name of an enum if <see cref="DisplayAttribute"/> is defined, otherwise, returns null.
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static string GetDisplayName(this Enum @enum)
    {
        var displayAttribute = @enum.GetAttribute<DisplayAttribute>();
        return displayAttribute?.Name;
    }

    /// <summary>
    /// Returns the display name of an enum if <see cref="DisplayAttribute"/> is defined, otherwise, returns
    /// the enum name via .ToString() method.
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static string GetDisplayNameOrDefault(this Enum @enum)
    {
        var displayAttribute = @enum.GetAttribute<DisplayAttribute>();
        return displayAttribute?.Name ?? @enum.ToString();
    }
}