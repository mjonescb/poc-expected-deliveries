namespace ApplicationServices
{
    using Domain.Bases;
    using Domain.PurchaseOrder.Events;

    public class PurchaseOrderEventHandlers : IEventHandler<CancellationSuggestedEvent>
    {
        public void Handle(CancellationSuggestedEvent @event)
        {
            // var thing = new Suggestion();
            // thing.Create();
        }
    }
}
