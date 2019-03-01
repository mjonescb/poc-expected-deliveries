namespace Domain.Infrastructure
{
    using System.Threading.Tasks;
    using Bases;

    public interface IAggregateRootFactory
    {
        Task<TAggregate> LoadAsync<TAggregate, TState, TKey>(TKey id)
            where TAggregate : AggregateRoot<TState, TKey>
            where TState : AggregateRootState<TKey>;
    }
}
