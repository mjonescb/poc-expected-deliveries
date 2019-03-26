namespace Process.Features.PurchaseOrderLine
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Pipeline;
    using Domain.PurchaseOrderLine;
    using Ports;

    public class Create
    {
        public class Command : IRequest<CommandResult>
        {
            public int PurchaseOrderLineId { get; set; }
            public DateTime DeliveryDate { get; set; }
            public int Quantity { get; set; }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            readonly IDocumentStore docStore;

            public Handler(IDocumentStore docStore)
            {
                this.docStore = docStore;
            }

            public async Task<CommandResult> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                PurchaseOrderLine aggregate = new PurchaseOrderLine(
                    request.PurchaseOrderLineId,
                    request.DeliveryDate,
                    request.Quantity);

                await docStore.StoreAsync(
                    request.PurchaseOrderLineId.ToString(),
                    aggregate);

                return CommandResult.Void
                    .WithNotification(
                        new CreatedEvent
                        {
                            PurchaseOrderLineId = request.PurchaseOrderLineId,
                            DeliveryExpected = request.DeliveryDate
                        });
            }
        }

        public class CreatedEvent : INotification
        {
            public int PurchaseOrderLineId { get; set; }
            public DateTime DeliveryExpected { get; set; }
        }
    }
}
