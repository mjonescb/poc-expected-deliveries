namespace Domain.Bases
{
    using System.Threading.Tasks;
    using Infrastructure;

    public abstract class AggregateRoot<TSnapshot>
        where TSnapshot : AggregateRootState
    {
        readonly ISendEvents publisher;

        protected TSnapshot Snapshot { get; set; }

        protected AggregateRoot(ISendEvents publisher)
        {
            this.publisher = publisher;
        }

        protected async Task EmitAsync<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            // get the potential new state
            TSnapshot newState = UpdateState(@event);
            await publisher.SendAsync(@event);
            Snapshot = newState;
        }

        protected abstract TSnapshot UpdateState<TEvent>(TEvent @event)
            where TEvent : IEvent;

        public void Load(TSnapshot state)
        {
            Snapshot = state;
        }
    }
}
