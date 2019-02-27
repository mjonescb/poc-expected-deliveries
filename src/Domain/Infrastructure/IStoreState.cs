namespace Domain.Infrastructure
{
    using System.Threading.Tasks;
    using Bases;

    public interface IStoreState
    {
        Task StoreAsync<TSnapshot>(TSnapshot state)
            where TSnapshot : AggregateRootState;
    }
}