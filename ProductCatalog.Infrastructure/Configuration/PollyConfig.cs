using FluentValidation;

namespace ProductCatalog.Infrastructure.Configuration;

public class PollyConfig
{
    public int NoOfRetries { get; set; }
}

public class PollyConfigValidator : AbstractValidator<PollyConfig>
{
    public PollyConfigValidator()
    {
        RuleFor(i => i.NoOfRetries).NotNull().GreaterThan(0);
    }
}