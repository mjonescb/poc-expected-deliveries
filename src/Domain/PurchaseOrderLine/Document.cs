namespace Domain.PurchaseOrderLine
{
    using System;

    public class Document
    {
        public int Id { get; set; }
        public State State { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public int ExpectedQuantity { get; set; }
    }
}