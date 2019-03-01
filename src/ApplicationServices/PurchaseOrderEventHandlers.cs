namespace ApplicationServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.ExpectedDelivery.Events;
    using Domain.Infrastructure;
    using Domain.Suggestion;
    using Domain.Suggestion.Commands;
    using MediatR;

    public class PurchaseOrderEventHandlers :
        INotificationHandler<CancellationSuggestedEvent>,
        INotificationHandler<CreatedEvent>
    {
        readonly IAggregateRootFactory factory;
        readonly IScheduleCommands commandScheduler;

        public PurchaseOrderEventHandlers(
            IAggregateRootFactory factory,
            IScheduleCommands commandScheduler)
        {
            this.factory = factory;
            this.commandScheduler = commandScheduler;
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

        public async Task Handle(
            CreatedEvent notification,
            CancellationToken cancellationToken)
        {
            await commandScheduler.SetCallbackAsync(
                "0 6 * * *", // daily at 6am
                "/api/expected-deliveries/1/reviews", // url
                "POST", // HTTP method
                new { }); // object to return
        }
    }
}
