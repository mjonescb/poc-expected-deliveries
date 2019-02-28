namespace Domain.ExpectedDelivery.Events
{
    using Bases;

    public class CancellationSuggestedEvent : IEvent
    {
        public int PurchaseOrderLineId { get; set; }
    }
}
