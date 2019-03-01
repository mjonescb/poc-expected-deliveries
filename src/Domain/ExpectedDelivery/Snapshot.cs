namespace Domain.ExpectedDelivery
{
    using System;
    using Bases;

    public class Snapshot : AggregateRootState<int>
    {
        public static readonly Snapshot Default = new Snapshot()
        {
            State = State.None,
            ExpectedDeliveryDate = default(DateTime?)
        };

        public State State { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }
    }
}
