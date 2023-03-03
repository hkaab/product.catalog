using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using static System.StringComparison;

namespace EOS.Api.ServiceCollectionExtensions
{
  [ExcludeFromCodeCoverage]
  public static class Environments
  {
    public static bool IsLocalDevelopment(this IWebHostEnvironment env)
    {
      return env.EnvironmentName.Equals("Local", OrdinalIgnoreCase);
    }
  }
}