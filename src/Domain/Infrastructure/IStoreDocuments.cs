namespace Domain.Infrastructure
{
    using System.Threading.Tasks;
    using Bases;

    public interface IStoreDocuments
    {
        Task StoreAsync<TState, TKey>(TState state)
            where TState : AggregateRootState<TKey>;

        Task<TState> GetAsync<TState, TKey>(TKey key)
            where TState : AggregateRootState<TKey>;
    }
}