namespace Process.Features.PurchaseOrderLine
{
    using MediatR;
    using Pipeline;

    public class Cancel
    {
        public class Command : IRequest<CommandResult>
        {
            public int PurchaseOrderLineId { get; set; }
        }
    }
}
