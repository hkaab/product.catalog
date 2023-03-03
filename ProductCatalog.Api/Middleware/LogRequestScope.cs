using ProductCatalog.Infrastructure.Configuration;

namespace ProductCatalog.Api.Middleware;

public class LogRequestScope
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogRequestScope> _logger;
    private readonly AppConfig _config;

    public LogRequestScope(ILogger<LogRequestScope> logger, RequestDelegate next, AppConfig config)
    {
        _logger = logger;
        _next = next;
        _config = config;
    }

    public async Task Invoke(HttpContext context)
    {
        using (_logger.BeginScope("{ApiVersion}", _config.APIVersion))
        {
            await _next(context);
        }
    }
}