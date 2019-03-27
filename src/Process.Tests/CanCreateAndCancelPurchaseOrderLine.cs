namespace Process.Tests
{
    using System;
    using System.Threading.Tasks;
    using Bases;
    using Features.PurchaseOrderLine;
    using Xunit;

    public class CanCreateAndCancelPurchaseOrderLine : ProcessTestBase
    {
        [Fact]
        public async Task Test()
        {
            await Mediator().Send(new Create.Command
            {
                PurchaseOrderLineId = 1,
                Quantity = 100,
                DeliveryDate = DateTime.Today.AddDays(1)
            });

            Assert.True(Notifications().WasReceived<Create.CreatedEvent>());
            
            await Mediator().Send(new Cancel.Command
            {
                PurchaseOrderLineId = 1
            });

            Assert.True(Notifications().WasReceived<Cancel.CancelledEvent>());
        }
    }
}
