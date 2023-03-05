using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductCatalog.Api.Controllers;
using ProductCatalog.Interfaces;
using ProductCatalog.Models;

namespace UnitTests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ILogger<ProductController>> _mockLogger;
        private readonly ProductController _productController;
        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockLogger = new Mock<ILogger<ProductController>>();
            _productController = new ProductController(_mockProductService.Object, _mockLogger.Object);
        }

        [Fact]
        public async void GetProductsAsync_WhenCalledWithValidParam_ReturnsProductCollection()
        {
            //Arrange
            var expected = new List<Product> { 
                new Product {
                    Name = "FakeProduct", 
                    Price = 10, 
                    Quantity = 0
                } 
            };

            _mockProductService.Setup(service => service.GetProductsAsync("Name", ProductCatalog.Models.Enums.SortOptions.Ascending, CancellationToken.None)).Returns(Task.FromResult((IEnumerable<Product>)expected));

            //Act
            var result = (ObjectResult) await _productController.GetProductsAsync(CancellationToken.None);

            //Assert
            Assert.IsType<List<Product>>(result.Value);
            Assert.Equal(expected, result.Value);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async void GetProductsAsync_WhenException_ReturnsStatus500InternalServerError()
        {
            //Arrange
            var expected = "Internal Server Error";
            _mockProductService.Setup(service => service.GetProductsAsync("Name",ProductCatalog.Models.Enums.SortOptions.Ascending,CancellationToken.None)).Throws(new Exception(expected));
            //Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _productController.GetProductsAsync(CancellationToken.None));
            Assert.Equal(expected, exception.Message);
        }
    }
}
