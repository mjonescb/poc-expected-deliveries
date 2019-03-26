namespace Process.Features.PurchaseOrderLine
{
    using MediatR;
    using Pipeline;

    public class Review 
    {
        public class Command: IRequest<CommandResult>
        {
            public int PurchaseOrderLineId { get; set; }
        }
    }
}
