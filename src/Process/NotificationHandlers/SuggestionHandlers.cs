namespace Process.NotificationHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Features.PurchaseOrderLine;
    using MediatR;

    public class SuggestionHandlers :
        INotificationHandler<Review.CancellationSuggestedEvent>
    {
        readonly IMediator mediator;

        public SuggestionHandlers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(
            Review.CancellationSuggestedEvent notification,
            CancellationToken cancellationToken)
        {
            await mediator.Send(new Process.Features.Suggestion.Create.Command
            {
                PurchaseOrderLineId = notification.PurchaseOrderLineId
            });
        }
    }
}
