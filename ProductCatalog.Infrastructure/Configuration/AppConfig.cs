using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace ProductCatalog.Infrastructure.Configuration;

public class AppConfig
{
    //Environment Names
    public const string Local = nameof(Local);
    public string APIVersion { get; set; }
    public string GitHash { get; set; }
    public string Environment { get; set; }
    public ExternalApiConfig ExternalApiConfig { get; set; }
    public JwtTokenValidationConfig JwtTokenValidation { get; set; }
    public AuthorizationConfig Authorization { get; set; }
    public PollyConfig PollyConfig { get; set; }

    public IConfigurationSection IpRateLimiting { get; set; }
    public class AppConfigValidator : AbstractValidator<AppConfig>
    {
        public AppConfigValidator()
        {
            RuleFor(i => i.JwtTokenValidation).NotNull()
              .SetValidator(new JwtTokenValidationConfigValidator());

            RuleFor(i => i.Authorization).NotNull()
              .SetValidator(new AuthorizationConfigValidator());

            RuleFor(i => i.PollyConfig).NotNull()
              .SetValidator(new PollyConfigValidator());

            RuleFor(i => i.ExternalApiConfig).NotNull()
              .SetValidator(new ExternalApiConfigValidator());

        }
    }
}