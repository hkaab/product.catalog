using Microsoft.Extensions.Logging;
using Moq;
using ProductCatalog.Infrastructure.Configuration;
using ProductCatalog.Interfaces;
using ProductCatalog.Models;
using ProductCatalog.Services;
using UnitTests.Builders;

namespace UnitTests.Services
{
    public class ProductServiceTests
    {
        public ProductServiceTests()
        {
        }

        [Fact]
        public async void GetArtistByIdAsync_WhenCalledWithValidParam_ReturnsArtist()
        {
            var externalApiService = new ExternalServiceBuilder().ForGetProducts().Build();
            var _mockShopperHistoryService = new Mock<IShopperHistoryService>();
            var _appConfing = new AppConfig();
            var _mockLogger = new Mock<ILogger<ProductService>>();
            _appConfing.ExternalApiConfig = new ExternalApiConfig
            {
                Name = "WooliesX",
                ProductsUri = "products"
            };
            var productService = new ProductService(_mockShopperHistoryService.Object, externalApiService, _appConfing, _mockLogger.Object);
            var result = await productService.GetProductsAsync("Name",ProductCatalog.Models.Enums.SortOptions.Recommended,CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
        }
    }
}
