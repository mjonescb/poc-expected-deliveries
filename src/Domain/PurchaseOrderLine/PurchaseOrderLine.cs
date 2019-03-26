namespace Domain.PurchaseOrderLine
{
    using System;

    public class PurchaseOrderLine
    {
        readonly int id;
        readonly DateTime expectedDeliveryDate;
        readonly int expectedQuantity;

        bool isCancelled;

        public PurchaseOrderLine(
            int id,
            DateTime expectedDeliveryDate,
            int expectedQuantity)
        {
            this.id = id;
            this.expectedDeliveryDate = expectedDeliveryDate;
            this.expectedQuantity = expectedQuantity;
        }

        public void Cancel()
        {
            isCancelled = true;
        }
    }
}
