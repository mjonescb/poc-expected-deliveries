namespace Domain.Bases
{
    using System.Threading.Tasks;
    using Infrastructure;

    public abstract class AggregateRoot<TSnapshot>
        where TSnapshot : AggregateRootState
    {
        readonly ISendEvents publisher;
        readonly IStoreDocuments documentStore;

        protected TSnapshot Snapshot { get; set; }

        protected AggregateRoot(
            ISendEvents publisher,
            IStoreDocuments documentStore)
        {
            this.publisher = publisher;
            this.documentStore = documentStore;
        }

        protected async Task EmitAsync<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            TSnapshot newState = UpdateState(@event);
            await documentStore.StoreAsync(newState);
            Snapshot = newState;
            await publisher.SendAsync(@event);
        }

        protected abstract TSnapshot UpdateState<TEvent>(TEvent @event)
            where TEvent : IEvent;

        public void Load(TSnapshot state)
        {
            Snapshot = state;
        }
    }
}
