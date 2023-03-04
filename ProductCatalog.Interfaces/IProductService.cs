using ProductCatalog.Models;
using ProductCatalog.Models.Enums;

namespace ProductCatalog.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetProductsAsync(string attribute, SortOptions? sortOptions, CancellationToken cancellationToken);
    }
}