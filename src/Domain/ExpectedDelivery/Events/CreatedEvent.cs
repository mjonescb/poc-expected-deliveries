namespace Domain.ExpectedDelivery.Events
{
    using System;
    using Bases;

    public class CreatedEvent : IEvent
    {
        public int PurchaseOrderLineId { get; set; }
        public DateTime DeliveryExpected { get; set; }
    }
}