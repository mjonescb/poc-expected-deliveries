namespace Domain.PurchaseOrderLine.Commands
{
    using MediatR;

    public class ReviewCommand : IRequest
    {
        public int PurchaseOrderLineId { get; set; }
    }
}
