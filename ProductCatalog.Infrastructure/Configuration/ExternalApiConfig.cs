using FluentValidation;

namespace ProductCatalog.Infrastructure.Configuration;

public class ExternalApiConfig
{
    public string BaseUrl { get; set; }
    public string TrolleyUri { get; set; }
}
public class ExternalApiConfigValidator : AbstractValidator<ExternalApiConfig>
{
    public ExternalApiConfigValidator()
    {
        RuleFor(i => i.BaseUrl).NotNull().NotEmpty();
        RuleFor(i => i.TrolleyUri).NotNull().NotEmpty();
    }
}
