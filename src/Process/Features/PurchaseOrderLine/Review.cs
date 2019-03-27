namespace Process.Features.PurchaseOrderLine
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Pipeline;
    using Ports;
    using Domain.PurchaseOrderLine;

    public class Review 
    {
        public class Command : Pipeline.Command
        {
            public int PurchaseOrderLineId { get; set; }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            readonly IDocumentStore store;

            public Handler(IDocumentStore store)
            {
                this.store = store;
            }

            public async Task<CommandResult> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                PurchaseOrderLine subject = await store
                    .GetAsync<PurchaseOrderLine>(request.PurchaseOrderLineId.ToString());

                ReviewOutcome reviewResult = subject.Review();

                CommandResult result = CommandResult.Void;
                
                if(reviewResult is CancellationSuggested)
                {
                    result = result
                        .WithNotification(
                            new CancellationSuggestedEvent
                            {
                                PurchaseOrderLineId = request.PurchaseOrderLineId
                            });
                }

                return result;
            }
        }

        public class CancellationSuggestedEvent : INotification
        {
            public int PurchaseOrderLineId { get; set; }
        }
    }
}
