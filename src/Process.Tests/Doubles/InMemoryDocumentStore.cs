namespace Process.Tests.Doubles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Utilities;
    using Ports;

    public class InMemoryDocumentStore : IDocumentStore
    {
        readonly IDictionary<string, object> innerDict;

        public InMemoryDocumentStore()
        {
            innerDict = new Dictionary<string, object>();
        }

        public Task StoreAsync<TDocument>(string key, TDocument toStore)
        {
            Ensure.IsNotNull(toStore, nameof(toStore));
            Ensure.IsNotNullOrEmpty(key, nameof(key));

            if(innerDict.ContainsKey(key))
            {
                innerDict[key] = toStore;
                return Task.CompletedTask;
            }

            innerDict.Add(key, toStore);
            return Task.CompletedTask;
        }

        public Task<TDocument> GetAsync<TDocument>(string key)
        {
            TDocument result = default(TDocument);
            
            if(innerDict.ContainsKey(key))
            {
                result = (TDocument) innerDict[key];
            }

            return Task.FromResult(result);
        }

        public Task<bool> ExistsAsync(string key)
        {
            return Task.FromResult(innerDict.ContainsKey(key));
        }
    }
}