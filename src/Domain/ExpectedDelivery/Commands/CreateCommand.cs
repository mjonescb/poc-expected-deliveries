namespace Domain.ExpectedDelivery.Commands
{
    using System;

    public class CreateCommand
    {
        public int PurchaseOrderLineId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
    }
}