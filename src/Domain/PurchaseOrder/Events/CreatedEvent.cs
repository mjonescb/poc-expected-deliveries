namespace Domain.PurchaseOrder.Events
{
    using System;
    using Bases;

    public class CreatedEvent : IEvent
    {
        public int Id { get; set; }
        public DateTime DeliveryExpected { get; set; }
    }
}