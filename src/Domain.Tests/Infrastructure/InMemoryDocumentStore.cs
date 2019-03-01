namespace Domain.Tests.Infrastructure
{
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Bases;
    using Domain.Infrastructure;

    public class InMemoryDocumentStore : IStoreDocuments
    {
        readonly ConcurrentDictionary<string, object> dict;

        public InMemoryDocumentStore()
        {
            dict = new ConcurrentDictionary<string, object>();
        }

        public Task StoreAsync<TState, TKey>(TState state)
            where TState : AggregateRootState<TKey>
        {
            dict.AddOrUpdate(state.Id.ToString(), state, (s, o) => state);

            return Task.CompletedTask;
        }

        public Task<TState> GetAsync<TState, TKey>(TKey key)
            where TState : AggregateRootState<TKey>
        {
            TState result = default(TState);

            if(dict.ContainsKey(key.ToString()))
            {
                result = (TState)dict[key.ToString()];
            }

            return Task.FromResult(result);
        }
    }
}
