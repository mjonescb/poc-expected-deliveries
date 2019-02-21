namespace Domain.PurchaseOrder
{
    using System;
    using Bases;

    public class Snapshot : AggregateRootState
    {
        public static readonly Snapshot Default = new Snapshot()
        {
            State = Domain.PurchaseOrder.State.None,
            ExpectedDeliveryDate = default(DateTime?)
        };

        public State State { get; private set; }

        public DateTime? ExpectedDeliveryDate { get; private set; }

        public Snapshot ChangeState(State state)
        {
            return new Snapshot
            {
                State = state,
                ExpectedDeliveryDate = ExpectedDeliveryDate
            };
        }

        public Snapshot SetExpectedDelivery(DateTime expectedDelivery)
        {
            return new Snapshot
            {
                State = State,
                ExpectedDeliveryDate = expectedDelivery
            };
        }
    }
}
