namespace Domain.Tests.ExpectedDelivery
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Domain.ExpectedDelivery.Commands;
    using Domain.ExpectedDelivery.Events;
    using Infrastructure;
    using Xunit;
    using Time;
    using ExpectedDelivery = Domain.ExpectedDelivery.ExpectedDelivery;

    public class Tests
    {
        readonly InMemoryDocumentStore store;
        readonly InMemoryEventBus bus;

        public Tests()
        {
            store = new InMemoryDocumentStore();
            bus = new InMemoryEventBus();
        }

        [Fact]
        [Category("Happy path")]
        public async Task CanCreateAndCancelOrder()
        {
            ExpectedDelivery sut = new ExpectedDelivery(bus, store);

            await sut.Handle(new CreateCommand
            {
                PurchaseOrderLineId = 123,
                DeliveryDate = new DateTime(2019, 2, 28)
            });

            await sut.Handle(new CancelCommand
            {
                PurchaseOrderLineId = 123
            });
            
            Assert.True(bus.WasRaised<CreatedEvent>());
            Assert.True(bus.WasRaised<CancelledEvent>());
        }

        [Fact]
        [Category("Happy path, Cancellation")]
        public async Task CancellationSuggestedOutsideWindow()
        {
            ExpectedDelivery sut = new ExpectedDelivery(bus, store);

            await sut.Handle(new CreateCommand
            {
                PurchaseOrderLineId = 123,
                DeliveryDate = DateTime.Today
            });

            using(Clock.Adjust(TimeSpan.FromDays(61)))
            {
                await sut.Handle(new ReviewCommand());

                Assert.True(bus.WasRaised<CancellationSuggestedEvent>());
            }
        }

        [Fact]
        [Category("Happy path, Cancellation")]
        public async Task NoCancellationSuggestedWithinWindow()
        {
            ExpectedDelivery sut = new ExpectedDelivery(bus, store);

            await sut.Handle(new CreateCommand
            {
                PurchaseOrderLineId = 123,
                DeliveryDate = DateTime.Today
            });

            using(Clock.Adjust(TimeSpan.FromDays(59)))
            {
                await sut.Handle(new ReviewCommand());

                Assert.True(bus.WasNotRaised<CancellationSuggestedEvent>());
            }
        }

        [Fact]
        [Category("Happy path, Cancellation")]
        public async Task NoCancellationSuggestedIfAlreadyCancelled()
        {
            ExpectedDelivery sut = new ExpectedDelivery(bus, store);

            await sut.Handle(new CreateCommand
            {
                PurchaseOrderLineId = 123,
                DeliveryDate = DateTime.Today
            });

            using(Clock.Adjust(TimeSpan.FromDays(3)))
            {
                await sut.Handle(new CancelCommand { PurchaseOrderLineId = 123 });
            }

            using(Clock.Adjust(TimeSpan.FromDays(70)))
            {
                await sut.Handle(new ReviewCommand());

                Assert.True(bus.WasNotRaised<CancellationSuggestedEvent>());
            }
        }

        [Fact]
        [Category("Exception cases")]
        public async Task CantCancelAnOrderBeforeCreated()
        {
            ExpectedDelivery sut = new ExpectedDelivery(bus, store);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                sut.Handle(new CancelCommand
                {
                    PurchaseOrderLineId = 123
                }));
        }
    }
}
