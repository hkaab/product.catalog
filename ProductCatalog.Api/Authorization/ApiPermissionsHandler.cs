using ProductCatalog.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace ProductCatalog.Api.Authorization
{
    public class ApiPermissionsHandler : AuthorizationHandler<ApiPermissionsRequirement>
  {
    private readonly AuthorizationConfig _authorizationConfig;
    private const string AppIdType = "appid";

    public ApiPermissionsHandler(AppConfig appConfig)
    {
      _authorizationConfig = appConfig.Authorization;
    }
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiPermissionsRequirement requirement)
    {
      if (!_authorizationConfig.EnableAuth)
      {
        context.Succeed(requirement);
        return Task.FromResult(0);
      }
      var appId = context.User?.FindFirst(c => c.Type == AppIdType)?.Value;
      var allConsumerList = new List<string>();
      allConsumerList.AddRange(_authorizationConfig.OnlineWooliesApp);
      var permissions = requirement.ApiPermission switch
      {
        ApiPermissions.OnlineWooliesApp => _authorizationConfig.OnlineWooliesApp,
        _ => null
      };
      if (permissions is not null && permissions.Any(app => app.Equals(appId)))
      {
        context.Succeed(requirement);
      }
      return Task.FromResult(0);
    }
  }
}
