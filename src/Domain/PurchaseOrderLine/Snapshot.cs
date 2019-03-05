namespace Domain.PurchaseOrderLine
{
    using System;
    using Bases;

    public class Snapshot : AggregateRootState<int>
    {
        public static readonly Snapshot Default = new Snapshot()
        {
            State = State.None,
            ExpectedDeliveryDate = default(DateTime?),
            ExpectedQuantity = 0
        };

        public State State { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }
        
        public int ExpectedQuantity { get; set; }
    }
}
