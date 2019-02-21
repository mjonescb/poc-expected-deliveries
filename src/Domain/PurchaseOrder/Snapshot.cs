namespace Domain.PurchaseOrder
{
    using System;
    using Bases;

    public class Snapshot : AggregateRootState
    {
        public PurchaseOrderState State { get; set; } = PurchaseOrderState.None;

        public DateTime? ExpectedDeliveryDate { get; set; } = default(DateTime?);
    }
}