namespace Process.Tests
{
    using System;
    using System.Threading.Tasks;
    using Aspects.Validation;
    using Bases;
    using Features.PurchaseOrderLine;
    using Xunit;

    public class CantCreateAPurchaseOrderWithZeroQuantity : ProcessTestBase
    {
        [Fact]
        public async Task Test()
        {
            await Assert.ThrowsAsync<PipelineValidationException>(() =>
                Mediator().Send(new Create.Command
                {
                    PurchaseOrderLineId = 1,
                    Quantity = 0,
                    DeliveryDate = DateTime.Today.AddDays(7)
                }));
        }
    }
}