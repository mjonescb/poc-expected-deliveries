namespace Process.Features.PurchaseOrderLine
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.PurchaseOrderLine;
    using FluentValidation;
    using MediatR;
    using Pipeline;
    using Ports;

    public class Cancel
    {
        public class Command : Pipeline.Command
        {
            public int PurchaseOrderLineId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            readonly IDocumentStore documentStore;

            public Validator(IDocumentStore documentStore)
            {
                this.documentStore = documentStore;
                
                RuleFor(x => x.PurchaseOrderLineId)
                    .MustAsync(Exist);
            }

            Task<bool> Exist(int arg1, CancellationToken arg2)
            {
                return documentStore.ExistsAsync(arg1.ToString());
            }
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
                Document doc = await store.GetAsync<Document>(
                    request.PurchaseOrderLineId.ToString());

                PurchaseOrderLine aggregate = new PurchaseOrderLine(doc);

                aggregate.Cancel();

                await store.StoreAsync(
                    request.PurchaseOrderLineId.ToString(),
                    aggregate.ToDocument());

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
