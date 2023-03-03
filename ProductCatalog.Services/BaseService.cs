using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ProductCatalog.Services;

public abstract class BaseService<T>
{
    protected readonly ILogger<T> Logger;
    protected readonly IHttpClientFactory HttpClientFactory;

    protected BaseService(IHttpClientFactory httpClientFactory,ILogger<T> logger)
    {
        Logger = logger;
        HttpClientFactory = httpClientFactory;
    }

}