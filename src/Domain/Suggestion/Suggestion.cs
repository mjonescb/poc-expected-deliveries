namespace Domain.Suggestion
{
    using System.Threading.Tasks;
    using Bases;
    using Commands;
    using Events;
    using Helpers;
    using Infrastructure;
    using MediatR;

    public class Suggestion : AggregateRoot<State, int>
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

        protected override State UpdateState<TEvent>(TEvent @event)
        {
            State newState = Snapshot;
            
            @event.Match()
                .When<CreateCommand>(e =>
                {
                    newState.Action = e.Action;
                    newState.PurchaseOrderLineId = e.PurchaseOrderLineId;
                });

            return newState;
        }
    }
}
