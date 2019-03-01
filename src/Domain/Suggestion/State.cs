namespace Domain.Suggestion
{
    using Bases;

    public class State : AggregateRootState<int>
    {
        public int PurchaseOrderLineId { get; set; }
        public string Action { get; set; }
    }
}