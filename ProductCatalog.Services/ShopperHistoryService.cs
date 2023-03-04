using Microsoft.Extensions.Logging;
using ProductCatalog.Infrastructure.Configuration;
using ProductCatalog.Interfaces;
using ProductCatalog.Library.Extensions;
using ProductCatalog.Models;
using System.Text.Json;

namespace ProductCatalog.Services
{
    public class ShopperHistoryService : BaseService<ShopperHistoryService>, IShopperHistoryService
    {
        protected readonly AppConfig _config;
        protected readonly ILogger<ShopperHistoryService> _logger;
        protected readonly IExternalApiService _externalApiService;
        public ShopperHistoryService(IExternalApiService externalApiService, AppConfig config, ILogger<ShopperHistoryService> logger) : base(logger)
        {
            _config = config;
            _logger = logger;
            _externalApiService = externalApiService;
        }

        public async Task<IEnumerable<ShopperHistory>> GetShoppersHistoryAsync(CancellationToken cancellationToken)
        {
            var response = await _externalApiService.GetAsync(_config.ExternalApiConfig.ShopperHistoryUri,cancellationToken);

            return Serialization.FromJson<IList<ShopperHistory>>(response);
        }


    }
}
