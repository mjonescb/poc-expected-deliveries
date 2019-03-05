namespace Domain.Bases
{
    using System.Threading.Tasks;
    using Infrastructure;
    using MediatR;

    public abstract class AggregateRoot<TSnapshot, TKey>
        where TSnapshot : AggregateRootState<TKey>
    {
        readonly IMediator publisher;
        readonly IStoreDocuments documentStore;

        protected TSnapshot Snapshot { get; set; }

        protected AggregateRoot(
            IMediator publisher,
            IStoreDocuments documentStore)
        {
            this.publisher = publisher;
            this.documentStore = documentStore;
        }

        protected async Task EmitAsync<TEvent>(TEvent @event)
            where TEvent : INotification
        {
            TSnapshot newState = UpdateState(@event);
            newState.IncrementVersion();
            await documentStore.StoreAsync<TSnapshot, TKey>(newState);
            Snapshot = newState;

            await publisher.Publish(@event);
        }

        protected abstract TSnapshot UpdateState<TEvent>(TEvent @event)
            where TEvent : INotification;

        public void Load(TSnapshot state)
        {
            Snapshot = state;
        }
    }
}
