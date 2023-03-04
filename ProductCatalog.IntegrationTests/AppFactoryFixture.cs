using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Infrastructure.Configuration;

namespace EOS.IntegrationTests.Library.Fixtures
{
    public class AppFactoryFixture : IDisposable
    {
        public readonly WebApplicationFactory<Program> factory;
        public AppFactoryFixture()
        {
            factory = new WebApplicationFactory<Program>();
        }

        public HttpClient Client => factory.CreateClient();

        public AppConfig AppConfig => factory.Services.GetRequiredService<AppConfig>();


        public IHttpClientFactory HttpClientFactory  => factory.Services.GetRequiredService<IHttpClientFactory>();

        public WebApplicationFactory<Program> Factory => factory;
        public void Dispose()
        {
            factory.Dispose();
        }
    }
}