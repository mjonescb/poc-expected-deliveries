namespace Domain.ExpectedDelivery
{
    using System;
    using Bases;

    public class Snapshot : AggregateRootState
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
