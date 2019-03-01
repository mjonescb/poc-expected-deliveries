namespace Domain.Suggestion.Events
{
    using MediatR;

    public class SuggestedCreatedEvent : INotification
    {
        public int PurchaseOrderLineId { get; set; }
        public string Action { get; set; }
    }
}