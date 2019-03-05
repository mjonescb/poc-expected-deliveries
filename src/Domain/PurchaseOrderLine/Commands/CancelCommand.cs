namespace Domain.PurchaseOrderLine.Commands
{
    using MediatR;

    public class CancelCommand : IRequest
    {
        public int PurchaseOrderLineId { get; set; }
    }
}