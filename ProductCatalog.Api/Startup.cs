using AspNetCoreRateLimit;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using ProductCatalog.Infrastructure.Configuration;
using ProductCatalog.Interfaces;
using ProductCatalog.Services;
using System.Text.Json.Serialization;
using System.Text.Json;
using ProductCatalog.Library;
using ProductCatalog.Library.Attributes;
using ProductCatalog.Api.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Authorization;
using ProductCatalog.Api.Authorization;

namespace ProductCatalog.Api;

public class Startup
{
    private readonly AppConfig _appConfig;
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        _appConfig = Configuration.Get<AppConfig>();
        var appConfigValidator = new AppConfig.AppConfigValidator();
        appConfigValidator.ValidateAndThrow(_appConfig);
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var enableAuth = _appConfig.Authorization.EnableAuth;

        if (enableAuth)
        {
            services.AddJwtAuthentication(_appConfig.JwtTokenValidation);
        }

        services.AddAuthorization(AddAuthorizationPolicies);
        services.AddSingleton<IAuthorizationHandler, ApiPermissionsHandler>();

        var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                                   .WaitAndRetryAsync(_appConfig.PollyConfig.NoOfRetries, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

        services.AddHttpClient(Constants.WooliesXHttpClientName, client =>
        {
            client.BaseAddress = new Uri(_appConfig.ExternalApiConfig.BaseUrl);
        }).AddPolicyHandler(retryPolicy);

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton(_appConfig);


        services.AddControllers()
      .AddJsonOptions(opts =>
      {
          opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
          opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
          opts.JsonSerializerOptions.WriteIndented = true;
          opts.JsonSerializerOptions.Converters.Add(new DateFormatConverter());
      });

        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(_appConfig.IpRateLimiting);
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        services.AddInMemoryRateLimiting();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WooliesX Product Api", Version = "v1" });
        });
    }

    private void AddAuthorizationPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(ApiPermissions.OnlineWooliesApp,
          policy => policy.Requirements.Add(new ApiPermissionsRequirement(ApiPermissions.OnlineWooliesApp)));
    }

}

