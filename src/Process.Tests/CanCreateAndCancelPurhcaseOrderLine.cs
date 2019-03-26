namespace Process.Tests
{
    using System;
    using System.Threading.Tasks;
    using Bases;
    using Features.PurchaseOrderLine;
    using IoC;
    using SimpleInjector.Lifestyles;
    using Xunit;

    public class CanCreateAndCancelPurhcaseOrderLine : ProcessTestBase
    {
        [Fact]
        public async Task Test()
        {
            using(AsyncScopedLifestyle.BeginScope(ContainerFactory.Instance))
            {
                await Mediator().Send(new Create.Command
                {
                    PurchaseOrderLineId = 1,
                    Quantity = 100,
                    DeliveryDate = DateTime.Today.AddDays(1)
                });

                Notifications().WasReceived<Create.CreatedEvent>();
                
                await Mediator().Send(new Cancel.Command
                {
                    PurchaseOrderLineId = 1,
                });

                Notifications().WasReceived<CancelledEvent>();
            }
        }
    }
}
