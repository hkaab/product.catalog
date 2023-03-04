using AspNetCoreRateLimit;
using EOS.IntegrationTests.Library.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProductCatalog.Library.Extensions;
using ProductCatalog.Models;

namespace IntegrationTests
{
    public class ApiIntegrationTests : IClassFixture<AppFactoryFixture>
    {
        private readonly AppFactoryFixture appFactoryFixture;
        public ApiIntegrationTests( AppFactoryFixture fixture)
        {
            appFactoryFixture = fixture;
        }
        [Fact]
        public void WhenAppIsStarted_IHttpClientFactoryCreateWooliesXHttpClientSuccessfully()
        {
            var externalHttpClient = appFactoryFixture.HttpClientFactory.CreateClient(appFactoryFixture.AppConfig.ExternalApiConfig.Name);
            Assert.Equal(appFactoryFixture.AppConfig.ExternalApiConfig.BaseUrl, externalHttpClient.BaseAddress.ToString());
        }

        [Fact]
        public void WhenAppIsStarted_IpRateLimitOptionsAreLoadedSuccessfully()
        {
            var options = appFactoryFixture.Factory.Services.GetRequiredService<IOptions<IpRateLimitOptions>>();
            var generalRule = options.Value.GeneralRules[0];

            Assert.Single(options.Value.GeneralRules);
            Assert.Equal("*", generalRule.Endpoint);
            Assert.Equal("1s", generalRule.Period);
            Assert.Equal(1, generalRule.Limit);
        }

        [Fact]
        public async Task WhenQueryProductAsyncRequest_WithArtistName_Status200IsReturnedWithArtistCollection()
        {
            var response = await appFactoryFixture.Client.GetAsync($"{appFactoryFixture.AppConfig.ExternalApiConfig.ProductsUri}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = Serialization.FromJson<IEnumerable<Product>>(json);
            Assert.True(result.Count() > 1);
        }

        [Fact]
        public async Task WhenQueryShopperHistoryAsyncRequest_Status200IsReturnedWithShopperHistoryCollection()
        {
            var response = await appFactoryFixture.Client.GetAsync($"{appFactoryFixture.AppConfig.ExternalApiConfig.ShopperHistoryUri}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = Serialization.FromJson<IEnumerable<ShopperHistory>>(json);

            Assert.True(result.Count() > 1);
        }

        [Fact]
        public async Task WhenGetArtistByIdAsyncRequest_WithNotExistedName_Status404NotFoundResult()
        {
            var response = await appFactoryFixture.Client.GetAsync("dummy");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task WhenExceedRateLimit_ExpectException()
        {
            var requests = new List<string> { "1", "2", "3", "4", "5" };

            var allTasks = requests.Select(n => Task.Run(async () =>
            {
                var result = await appFactoryFixture.Client.GetStringAsync("shopperHistory");

            })).ToList();

            async Task ConcurrentApiRequests() => await Task.WhenAll(allTasks);

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => ConcurrentApiRequests());
            Assert.Equal("Response status code does not indicate success: 429 (Too Many Requests).", exception.Message);
        }

    }
}
