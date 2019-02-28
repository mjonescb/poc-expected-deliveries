namespace Domain.Suggestion.Commands
{
    public class CreateCommand
    {
        public int PurchaseOrderLineId { get; set; }
        public string Action { get; set; }
    }
}