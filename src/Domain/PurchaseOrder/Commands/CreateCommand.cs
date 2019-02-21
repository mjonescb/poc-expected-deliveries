namespace Domain.PurchaseOrder.Commands
{
    using System;

    public class CreateCommand
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}