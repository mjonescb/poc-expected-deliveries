namespace Domain.ExpectedDelivery.Commands
{
    using System;
    using MediatR;

    public class CreateCommand : IRequest
    {
        public int PurchaseOrderLineId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
    }
}