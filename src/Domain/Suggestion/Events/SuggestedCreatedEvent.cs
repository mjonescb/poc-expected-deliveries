namespace Domain.Suggestion.Events
{
    using Bases;

    public class SuggestedCreatedEvent : IEvent
    {
        public int PurchaseOrderLineId { get; set; }
        public string Action { get; set; }
    }
}