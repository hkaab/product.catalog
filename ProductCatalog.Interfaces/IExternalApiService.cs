namespace ProductCatalog.Interfaces
{
    public interface IExternalApiService
    {
        public Task<string> GetAsync(string uri, CancellationToken cancellationToken);

    }
}