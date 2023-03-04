using FluentValidation;
using System.Text;

namespace ProductCatalog.Infrastructure.Configuration;

public class ExternalApiConfig
{
    public string Name { get; set; }
    public string BaseUrl { get; set; }
    public string ProductsUri { get; set; }
    public string TrolleyCalculatorUri { get; set; }
    public string ShopperHistoryUri {get; set; }

    public string Token { get; set; }
}
public class ExternalApiConfigValidator : AbstractValidator<ExternalApiConfig>
{
    public ExternalApiConfigValidator()
    {
        RuleFor(i => i.Name).NotNull().NotEmpty();
        RuleFor(i => i.ProductsUri).NotNull().NotEmpty();
        RuleFor(i => i.Token).NotNull().NotEmpty();
        RuleFor(i => i.BaseUrl).NotNull().NotEmpty();
        RuleFor(i => i.TrolleyCalculatorUri).NotNull().NotEmpty();
        RuleFor(i => i.ShopperHistoryUri).NotNull().NotEmpty();
    }
}
