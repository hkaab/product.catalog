using Microsoft.Extensions.Logging;
using ProductCatalog.Interfaces;

namespace ProductCatalog.Services;

public class UserService : BaseService<UserService>,IUserService
{
    public UserService (IHttpClientFactory httpClientFactory, ILogger<UserService> logger) : base(httpClientFactory, logger)
    { 
    }
}
