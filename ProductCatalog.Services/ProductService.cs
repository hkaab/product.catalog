using Microsoft.Extensions.Logging;
using ProductCatalog.Infrastructure.Configuration;
using ProductCatalog.Interfaces;
using ProductCatalog.Library.Extensions;
using ProductCatalog.Models;
using ProductCatalog.Models.Enums;

namespace ProductCatalog.Services
{
    public class ProductService : BaseService<ProductService>,IProductService
    {
        protected readonly AppConfig _config;
        protected readonly ILogger<ProductService> _logger;
        protected readonly IExternalApiService _externalApiService;
        protected readonly IShopperHistoryService _shopperHistoryService;
        public ProductService(IShopperHistoryService shopperHistoryService, IExternalApiService externalApiService, AppConfig config, ILogger<ProductService> logger) : base(logger)
        {
            _config = config;
            _logger = logger;
            _externalApiService = externalApiService;
            _shopperHistoryService = shopperHistoryService;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string attribute,SortOptions? sortOptions,CancellationToken cancellationToken)
        {
            var  products = await this.GetProductsAsync(cancellationToken);

            if (sortOptions == SortOptions.Recommended)
                return await Recommend(products, cancellationToken);
            else
                return products.OrderBy(attribute, sortOptions).ToList();
        }
        private async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var response = await _externalApiService.GetAsync(_config.ExternalApiConfig.ProductsUri, cancellationToken);

            return Serialization.FromJson<IList<Product>>(response);
        }

        private async Task<IEnumerable<Product>> Recommend(IEnumerable<Product> products, CancellationToken cancellationToken)
        {
            var shopperHistory = await _shopperHistoryService.GetShoppersHistoryAsync(cancellationToken);
            var orderedProducts = shopperHistory.SelectMany(x => x.Products).GroupBy(x => x.Name).Select(x => new Product
            {
                Name = x.First().Name,
                Quantity = x.Sum(p=>p.Quantity),
                Price = x.First().Price
            }).OrderByDescending(x => x.Quantity).ToList();

            var notOrdered = products.Where(p => orderedProducts.All(m => m.Name != p.Name)).ToList();

            return orderedProducts.Union(notOrdered).ToList();
        }

    }
}
