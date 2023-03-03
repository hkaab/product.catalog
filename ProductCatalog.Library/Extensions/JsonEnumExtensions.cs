using System;
using System.Linq;
using System.Runtime.Serialization;

namespace ProductCatalog.Library.Extensions;

public static class JsonEnumExtensions
{
    public static string ToEnumString<T>(this T type) where T : Enum 
    {
        var enumType = typeof (T);
        var name = Enum.GetName(enumType, type);
        var enumMemberAttribute =
                ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true))
                .FirstOrDefault();
            return enumMemberAttribute != null ? enumMemberAttribute.Value : type.ToString();
    }

    public static T ToEnum<T>(this string str)
    {
        var enumType = typeof(T);
        foreach (var name in Enum.GetNames(enumType))
        {
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            if (enumMemberAttribute.Value == str) return (T)Enum.Parse(enumType, name);
        }
        //throw exception or whatever handling you want or
        return default(T);
    }
}