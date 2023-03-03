
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using ProductCatalog.Infrastructure.Configuration;

namespace ProductCatalog.Api.ServiceCollectionExtensions;

public static class JwtAuthenticationExtension
{
    public static void AddJwtAuthentication(this IServiceCollection services, JwtTokenValidationConfig jwtTokenValidationConfig)
    {
        var audience = jwtTokenValidationConfig.Audience;
        var issuer = jwtTokenValidationConfig.Issuer;
        IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{issuer}/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());
        OpenIdConnectConfiguration openIdConfig = configurationManager.GetConfigurationAsync(CancellationToken.None).Result;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = issuer,
                  ValidAudience = audience,
                  IssuerSigningKeys = openIdConfig.SigningKeys
              };
              options.Authority = issuer;
              options.Audience = audience;

              options.Events = new JwtBearerEvents
              {
                  OnAuthenticationFailed = context =>
            {
                context.Response.Headers.Add(
            context.Exception.GetType() == typeof(SecurityTokenExpiredException)
              ? "Token-Expired"
              : "Invalid-Token", "true");
                return Task.CompletedTask;
            }
              };
          });
    }
}