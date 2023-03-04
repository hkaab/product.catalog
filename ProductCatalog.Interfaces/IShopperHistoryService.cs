using ProductCatalog.Models;

namespace ProductCatalog.Interfaces;

public interface IShopperHistoryService
{
    public Task<IEnumerable<ShopperHistory>> GetShoppersHistoryAsync(CancellationToken cancellationToken);
}