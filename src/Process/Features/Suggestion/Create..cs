namespace Process.Features.Suggestion
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Pipeline;
    using Domain.Suggestion;
    using Ports;

    public class Create
    {
        public class Command : Pipeline.Command
        {
            public int PurchaseOrderLineId { get; set; }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            readonly IDocumentStore documentStore;

            public Handler(IDocumentStore documentStore)
            {
                this.documentStore = documentStore;
            }

            public async Task<CommandResult> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                Suggestion aggregate = new Suggestion(request.PurchaseOrderLineId);

                await documentStore.StoreAsync(
                    request.PurchaseOrderLineId.ToString(),
                    aggregate.ToDocument());
                
                return CommandResult.Void;
            }
        }
    }
}
