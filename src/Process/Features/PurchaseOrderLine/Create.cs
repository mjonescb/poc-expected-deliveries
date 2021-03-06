namespace Process.Features.PurchaseOrderLine
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Pipeline;
    using Domain.PurchaseOrderLine;
    using Domain.Time;
    using FluentValidation;
    using Ports;

    public class Create
    {
        public class Command : Pipeline.Command
        {
            public int PurchaseOrderLineId { get; set; }
            public DateTime DeliveryDate { get; set; }
            public int Quantity { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            readonly IDocumentStore documentStore;
            
            public Validator(IDocumentStore documentStore)
            {
                this.documentStore = documentStore;
                
                RuleFor(x => x.PurchaseOrderLineId)
                    .MustAsync(NotAlreadyExist);
                
                RuleFor(x => x.DeliveryDate)
                    .Must(BeAfterToday);

                RuleFor(x => x.Quantity)
                    .Must(BeAPositiveInteger);
            }

            async Task<bool> NotAlreadyExist(
                int arg,
                CancellationToken cancellationToken)
            {
                bool result = await documentStore.ExistsAsync(arg.ToString());
                return !result;
            }

            bool BeAPositiveInteger(int arg)
            {
                return arg > 0;
            }

            bool BeAfterToday(DateTime arg)
            {
                return arg > Clock.Instance.Now;
            }
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
                    aggregate.ToDocument());

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
