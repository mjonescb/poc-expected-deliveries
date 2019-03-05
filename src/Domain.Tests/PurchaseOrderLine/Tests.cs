namespace Domain.Tests.PurchaseOrderLine
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Domain.PurchaseOrderLine;
    using Domain.PurchaseOrderLine.Commands;
    using Domain.PurchaseOrderLine.Events;
    using Infrastructure;
    using Xunit;
    using Time;

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
            PurchaseOrderLine sut = new PurchaseOrderLine(bus, store);

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
            PurchaseOrderLine sut = new PurchaseOrderLine(bus, store);

            await sut.Handle(new CreateCommand
            {
                PurchaseOrderLineId = 123,
                Quantity = 50,
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
            PurchaseOrderLine sut = new PurchaseOrderLine(bus, store);

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
            PurchaseOrderLine sut = new PurchaseOrderLine(bus, store);

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
            PurchaseOrderLine sut = new PurchaseOrderLine(bus, store);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                sut.Handle(new CancelCommand
                {
                    PurchaseOrderLineId = 123
                }));
        }
    }
}
