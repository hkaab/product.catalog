using System;
using Microsoft.Extensions.Hosting;

namespace ProductCatalog.Library.Extensions;

public static class HostBuilderExtensions
{
    public static IHost BuildWithAction(this IHostBuilder hostBuilder, Action<IServiceProvider> buildAction)
    {
        var host = hostBuilder.Build();
        buildAction(host.Services);
        return host;
    }
    
    public static IHost BuildWithAction(this IHostBuilder hostBuilder, Action buildAction)
    {
        var host = hostBuilder.Build();
        buildAction();
        return host;
    }
}