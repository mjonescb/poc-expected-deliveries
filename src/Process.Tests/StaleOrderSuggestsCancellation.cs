namespace Process.Tests
{
    using System;
    using System.Threading.Tasks;
    using Bases;
    using Domain.Time;
    using Features.PurchaseOrderLine;
    using Xunit;

    public class StaleOrderSuggestsCancellation : ProcessTestBase
    {
        [Fact]
        public async Task Test()
        {
            await Mediator().Send(new Create.Command
            {
                PurchaseOrderLineId = 1,
                Quantity = 100,
                DeliveryDate = DateTime.Today.AddDays(7)
            });

            using(Clock.Instance.Adjust(TimeSpan.FromDays(68)))
            {
                await Mediator().Send(new Review.Command
                {
                    PurchaseOrderLineId = 1
                });
            }
            
            Assert.True(Notifications().WasReceived<Review.CancellationSuggestedEvent>());
        }
    }
}