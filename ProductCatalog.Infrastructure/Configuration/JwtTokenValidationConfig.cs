using FluentValidation;

namespace ProductCatalog.Infrastructure.Configuration
{
  public class JwtTokenValidationConfig
  {
    public const string JwtTokenValidation = "JwtTokenValidation";
    
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Instance { get; set; }
    public string TenantId { get; set; }
    public string Domain { get; set; }
  }

  public class JwtTokenValidationConfigValidator : AbstractValidator<JwtTokenValidationConfig>
  {
    public JwtTokenValidationConfigValidator()
    {
      RuleFor(i => i.Issuer).NotNull().NotEmpty();
      RuleFor(i => i.Audience).NotNull().NotEmpty();
    }
  }
}