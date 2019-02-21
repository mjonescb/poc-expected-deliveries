namespace Domain.Bases
{
    using System.Threading.Tasks;
    using Infrastructure;

    public abstract class AggregateRoot<TSnapshot>
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
            await publisher.SendAsync(@event);
            UpdateState(@event);
        }

        protected abstract void UpdateState<TEvent>(TEvent @event)
            where TEvent : IEvent;

        public void Load(TSnapshot state)
        {
            Snapshot = state;
        }
    }
}
