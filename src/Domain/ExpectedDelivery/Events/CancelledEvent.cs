namespace Domain.ExpectedDelivery.Events
{
    using Bases;

    public class CancelledEvent : IEvent
    {
        public int Id { get; set; }
    }
}