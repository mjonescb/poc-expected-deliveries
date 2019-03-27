namespace Process.Features.PurchaseOrderLine
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.PurchaseOrderLine;
    using MediatR;
    using Pipeline;
    using Ports;

    public class Cancel
    {
        public class Command : IRequest<CommandResult>
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
                PurchaseOrderLine aggregate = await store
                    .GetAsync<PurchaseOrderLine>(
                        request.PurchaseOrderLineId.ToString());
                
                aggregate.Cancel();

                await store.StoreAsync(
                    request.PurchaseOrderLineId.ToString(),
                    aggregate);

                return CommandResult.Void
                    .WithNotification(
                        new CancelledEvent
                        {
                            Id = request.PurchaseOrderLineId
                        });
            }
        }
        
        public class CancelledEvent : INotification
        {
            public int Id { get; set; }
        }
    }
}
