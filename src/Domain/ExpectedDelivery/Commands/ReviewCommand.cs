namespace Domain.ExpectedDelivery.Commands
{
    using MediatR;

    public class ReviewCommand : IRequest
    {
        public int PurchaseOrderLineId { get; set; }
    }
}
