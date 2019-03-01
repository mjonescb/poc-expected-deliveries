namespace Domain.ExpectedDelivery.Events
{
    using MediatR;

    public class CancellationSuggestedEvent : INotification
    {
        public int PurchaseOrderLineId { get; set; }
    }
}
