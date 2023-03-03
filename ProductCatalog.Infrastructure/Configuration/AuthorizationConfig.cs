using System.Collections.Generic;
using FluentValidation;

namespace ProductCatalog.Infrastructure.Configuration
{
    public class AuthorizationConfig
    {
        public const string Authorization = "Authorization";

        public bool EnableAuth { get; set; } = true;
        public IList<string> OnlineWooliesApp { get; set; }
    }

    public class AuthorizationConfigValidator : AbstractValidator<AuthorizationConfig>
    {
        public AuthorizationConfigValidator()
        {
            RuleFor(i => i.OnlineWooliesApp).NotNull().NotEmpty();
        }
    }
}