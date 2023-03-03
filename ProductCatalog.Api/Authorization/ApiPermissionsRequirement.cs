using Microsoft.AspNetCore.Authorization;

namespace ProductCatalog.Api.Authorization;

public class ApiPermissionsRequirement : IAuthorizationRequirement
{
    public ApiPermissionsRequirement(string apiPermission)
    {
        ApiPermission = apiPermission;
    }
    public string ApiPermission { get; set; }
}