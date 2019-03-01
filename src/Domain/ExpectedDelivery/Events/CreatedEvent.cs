namespace Domain.ExpectedDelivery.Events
{
    using System;
    using MediatR;

    public class CreatedEvent : INotification
    {
        public int PurchaseOrderLineId { get; set; }
        public DateTime DeliveryExpected { get; set; }
    }
}