namespace Domain.ExpectedDelivery.Events
{
    using MediatR;

    public class CancelledEvent : INotification
    {
        public int Id { get; set; }
    }
}