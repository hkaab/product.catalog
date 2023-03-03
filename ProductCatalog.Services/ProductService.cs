using Microsoft.Extensions.Logging;
using ProductCatalog.Interfaces;
using ProductCatalog.Library;

namespace ProductCatalog.Services
{
    public class ProductService : BaseService<ProductService>,IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(IHttpClientFactory httpClientFactory,ILogger<ProductService> logger) : base(httpClientFactory,logger)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.WooliesXHttpClientName);
        }

    }
}
