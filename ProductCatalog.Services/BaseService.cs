using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ProductCatalog.Services;

public abstract class BaseService<T>
{
    protected BaseService(ILogger<T> logger)
    {
        Logger = logger;
    }

    protected ILogger<T> Logger { get; }

}