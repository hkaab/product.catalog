using Microsoft.Extensions.Logging;
using ProductCatalog.Interfaces;

namespace ProductCatalog.Services;

public class UserService : BaseService<UserService>,IUserService
{
    public UserService (ILogger<UserService> logger) : base(logger)
    { 
    }
}
