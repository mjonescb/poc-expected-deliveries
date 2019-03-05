namespace Domain.Suggestion
{
    using System.Threading.Tasks;
    using Bases;
    using Commands;
    using Events;
    using Helpers;
    using Infrastructure;
    using MediatR;

    public class Suggestion : AggregateRoot<Snapshot, int>
    {
        public Suggestion(
            IMediator publisher,
            IStoreDocuments documentStore) : base(publisher, documentStore)
        {
        }

        public async Task Handle(CreateCommand command)
        {
            await EmitAsync(new SuggestedCreatedEvent
            {
                PurchaseOrderLineId = command.PurchaseOrderLineId,
                Action = command.Action
            });
        }

        public Task Handle(AcceptCommand command)
        {
            return Task.CompletedTask;
        }

        public Task Handle(DeclineCommand command)
        {
            return Task.CompletedTask;
        }

        protected override Snapshot UpdateState<TEvent>(TEvent @event)
        {
            Snapshot newSnapshot = Snapshot;
            
            @event.Match()
                .When<CreateCommand>(e =>
                {
                    newSnapshot.Action = e.Action;
                    newSnapshot.PurchaseOrderLineId = e.PurchaseOrderLineId;
                });

            return newSnapshot;
        }
    }
}
