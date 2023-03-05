using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using ProductCatalog.Api;
using ProductCatalog.Api.Middleware;
using ProductCatalog.Infrastructure.Configuration;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

WebApplication app;

if (builder.Environment.IsDevelopment())
{
    var root = Directory.GetCurrentDirectory();
    var dotenv = Path.Combine(root, ".env");

    if (File.Exists(dotenv))
    {
        DotEnv.Load(dotenv);
    }

    ConfigureServices(builder);

    app = builder.Build();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    ConfigureServices(builder);
    app = builder.Build();
}

app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<HealthCheckMiddleware>();
app.UseMiddleware<LogRequestScope>();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.Run();

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Host.ConfigureAppConfiguration((ctx, builder) =>
    {
        builder.AddEnvironmentVariables();
    });
    var startup = new Startup(builder.Configuration);
    startup.ConfigureServices(builder.Services);
}

public partial class Program { }
