namespace Domain.Infrastructure
{
    using System.Threading.Tasks;
    using Bases;

    public interface IStoreDocuments
    {
        Task StoreAsync<TState>(TState state) where TState : AggregateRootState;
    }
}