using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using ProductCatalog.Infrastructure.Configuration;
using ProductCatalog.Interfaces;
using ProductCatalog.Models;
using ProductCatalog.Services;
using System.Text;


namespace UnitTests.Builders
{
    public class ExternalServiceBuilder
    {
        private ExternalApiService externalApiService;
        private object _returnedObject;
        public ExternalServiceBuilder()
        {
            _returnedObject = new();
        }
        public ExternalServiceBuilder ForGetProducts()
        {
            _returnedObject = new List<Product> {
            new Product {
                Name = "Product 1",
                Price = 10.30,
                Quantity = 0
            }
            };

            return this;
        }

        public ExternalApiService Build()
        {
            var json = JsonConvert.SerializeObject(_returnedObject);
            var fakeUrl = "https://wooliesx.local/ws/2/";

            HttpResponseMessage httpResponse = new()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            Mock<HttpMessageHandler> mockHandler = new();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri.ToString().StartsWith(fakeUrl)),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            HttpClient httpClient = new(mockHandler.Object)
            {
                BaseAddress = new Uri(fakeUrl)

            };
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient("WooliesX")).Returns(httpClient);

            AppConfig appConfig = new AppConfig();
            appConfig.ExternalApiConfig = new ExternalApiConfig { Name = "WooliesX" };

            Mock<ILogger<ExternalApiService>> logger = new Mock<ILogger<ExternalApiService>>();
            
            externalApiService = new ExternalApiService(mockHttpClientFactory.Object,appConfig,logger.Object);

            return externalApiService;
        }
    }
}
