using Microsoft.Extensions.Logging;
using ProductCatalog.Infrastructure.Configuration;
using ProductCatalog.Infrastructure.Exceptions;
using ProductCatalog.Interfaces;
using System.Net;

namespace ProductCatalog.Services
{
    public class ExternalApiService : BaseService<ExternalApiService>, IExternalApiService
    {
        private readonly HttpClient _httpClient;
        protected readonly AppConfig _config;

        public ExternalApiService(IHttpClientFactory httpClientFactory, AppConfig config, ILogger<ExternalApiService> logger) : base(logger)
        {
            _httpClient = httpClientFactory.CreateClient(config.ExternalApiConfig.Name);
            _config = config;
        }

        public async Task<string> GetAsync(string uri, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(AddTokenToUri(uri), cancellationToken);
            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                throw new NotFoundException("Products Not found");
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        private string AddTokenToUri(string uri)
        {
            return $"{uri}?token={_config.ExternalApiConfig.Token}";
        }
    }
}
