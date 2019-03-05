namespace Domain.Suggestion
{
    using Bases;

    public class Snapshot : AggregateRootState<int>
    {
        public int PurchaseOrderLineId { get; set; }
        public string Action { get; set; }
        public State State { get; set; }
    }
}