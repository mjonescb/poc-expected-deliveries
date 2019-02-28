namespace ApplicationServices
{
    using System.Threading.Tasks;
    using Domain.Bases;
    using Domain.ExpectedDelivery.Events;
    using Domain.Infrastructure;
    using Domain.Suggestion;
    using Domain.Suggestion.Commands;

    public class PurchaseOrderEventHandlers : IEventHandler<CancellationSuggestedEvent>
    {
        readonly IAggregateRootFactory factory;

        public PurchaseOrderEventHandlers(IAggregateRootFactory factory)
        {
            this.factory = factory;
        }

        public async Task HandleAsync(CancellationSuggestedEvent @event)
        {
            Suggestion suggestion = factory.Create<Suggestion, State>();

            await suggestion.Handle(new CreateCommand
            {
                Action = "create",
                PurchaseOrderLineId = @event.PurchaseOrderLineId
            });
        }
    }
}
