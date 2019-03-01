namespace ApplicationServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.ExpectedDelivery.Events;
    using Domain.Infrastructure;
    using Domain.Suggestion;
    using Domain.Suggestion.Commands;
    using MediatR;

    public class PurchaseOrderEventHandlers : INotificationHandler<CancellationSuggestedEvent>
    {
        readonly IAggregateRootFactory factory;

        public PurchaseOrderEventHandlers(IAggregateRootFactory factory)
        {
            this.factory = factory;
        }

        public async Task Handle(
            CancellationSuggestedEvent notification,
            CancellationToken cancellationToken)
        {
            Suggestion suggestion = await factory
                .LoadAsync<Suggestion, State, int>(
                    notification.PurchaseOrderLineId);

            await suggestion.Handle(new CreateCommand
            {
                Action = "create",
                PurchaseOrderLineId = notification.PurchaseOrderLineId
            });
        }
    }
}
