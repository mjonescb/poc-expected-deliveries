namespace Domain.ExpectedDelivery.Commands
{
    using System;

    public class CreateCommand
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}