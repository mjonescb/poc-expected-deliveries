namespace Domain.ExpectedDelivery.Commands
{
    using MediatR;

    public class CancelCommand : IRequest
    {
        public int PurchaseOrderLineId { get; set; }
    }
}