namespace Domain.Suggestion.Commands
{
    using MediatR;

    public class CreateCommand : IRequest
    {
        public int PurchaseOrderLineId { get; set; }
        public string Action { get; set; }
    }
}