using System;
using System.Linq;

namespace ProductCatalog.Library.Extensions
{
  public static class StringExtensions
  {
    public static string ToLowerFirstChar(this string input)
    {
      if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
        return input;

      return char.ToLower(input[0]) + input[1..];
    }
    
    public static string ToLowerFirstCharAfterPeriod(this string input)
    {
      if(string.IsNullOrEmpty(input))
        return input;

      var result = input
        .Split('.')
        .Aggregate("", (current, word) => current + "." + char.ToLower(word[0]) + word[1..]);

      return result.Remove(0, 1);
    }
    
    public static TEnum? TryGetEnum<TEnum>(this string input) where TEnum : struct
    {
      if (Enum.TryParse(input, out TEnum result))
      {
        return result;
      }
      return null;
    }
    
    public static DateTime? TryGetDateTime(this string input, bool useDefault = false)
    {
      if (DateTime.TryParse(input, out var result))
      {
        return result;
      }
      return useDefault ? DateTime.Now : null;
    }
        
    /// <summary>
    /// Generates a random alphanumeric string.
    /// </summary>
    /// <param name="length">The desired length of the string</param>
    /// <returns>The string which has been generated</returns>
    public static string GenerateRandomAlphanumericString(int length = 10)
    {
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
 
      var random       = new Random();
      var randomString = new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
      return randomString;
    }
  }
}