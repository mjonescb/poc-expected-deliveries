namespace Domain.PurchaseOrder
{
    using System;
    using Bases;

    public class Snapshot : AggregateRootState
    {
        public State State { get; set; } = State.None;

        public DateTime? ExpectedDeliveryDate { get; set; } = default(DateTime?);
    }
}