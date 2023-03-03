using System.Linq;
using ProductCatalog.Library.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductCatalog.Library.Attributes;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var modelState = context.ModelState;
        if (modelState.IsValid) return;
        var modelErrors = modelState.Values
            .SelectMany(ms => ms.Errors)
            .Select(x => x.ErrorMessage);
        throw new BadRequestException(modelErrors);
    }
}