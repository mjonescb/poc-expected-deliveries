namespace Process.Ports
{
    using System.Threading.Tasks;

    public interface IDocumentStore
    {
        Task StoreAsync<TDocument>(string key, TDocument toStore);
        Task<TDocument> GetAsync<TDocument>(string key);
        Task<bool> ExistsAsync(string key);
    }
}