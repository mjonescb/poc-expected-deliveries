namespace Domain.Tests
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Xunit;
    using Infrastructure;
    using PurchaseOrder;
    using PurchaseOrder.Commands;
    using PurchaseOrder.Events;
    using Time;

    public class PurchaseOrderTests
    {
        [Fact]
        [Category("Happy path")]
        public async Task CanCreateAndCancelOrder()
        {
            InMemorySendEvents bus = new InMemorySendEvents();
            PurchaseOrder sut = new PurchaseOrder(bus);

            await sut.Handle(new CreateCommand
            {
                Id = 123,
                DeliveryDate = new DateTime(2019, 2, 28)
            });

            await sut.Handle(new CancelCommand
            {
                Id = 123
            });
            
            Assert.True(bus.WasRaised<CreatedEvent>());
            Assert.True(bus.WasRaised<CancelledEvent>());
        }

        [Fact]
        [Category("Happy path, Cancellation")]
        public async Task CancellationSuggestedOutsideWindow()
        {
            InMemorySendEvents bus = new InMemorySendEvents();
            PurchaseOrder sut = new PurchaseOrder(bus);

            await sut.Handle(new CreateCommand
            {
                Id = 123,
                DeliveryDate = DateTime.Today
            });

            using(Clock.Adjust(TimeSpan.FromDays(61)))
            {
                await sut.Handle(new CheckCommand());

                Assert.True(bus.WasRaised<CancellationSuggestedEvent>());
            }
        }

        [Fact]
        [Category("Happy path, Cancellation")]
        public async Task NoCancellationSuggestedWithinWindow()
        {
            InMemorySendEvents bus = new InMemorySendEvents();
            PurchaseOrder sut = new PurchaseOrder(bus);

            await sut.Handle(new CreateCommand
            {
                Id = 123,
                DeliveryDate = DateTime.Today
            });

            using(Clock.Adjust(TimeSpan.FromDays(59)))
            {
                await sut.Handle(new CheckCommand());

                Assert.True(bus.WasNotRaised<CancellationSuggestedEvent>());
            }
        }

        [Fact]
        [Category("Happy path, Cancellation")]
        public async Task NoCancellationSuggestedIfAlreadyCancelled()
        {
            InMemorySendEvents bus = new InMemorySendEvents();
            PurchaseOrder sut = new PurchaseOrder(bus);

            await sut.Handle(new CreateCommand
            {
                Id = 123,
                DeliveryDate = DateTime.Today
            });

            using(Clock.Adjust(TimeSpan.FromDays(3)))
            {
                await sut.Handle(new CancelCommand { Id = 123 });
            }

            using(Clock.Adjust(TimeSpan.FromDays(70)))
            {
                await sut.Handle(new CheckCommand());

                Assert.True(bus.WasNotRaised<CancellationSuggestedEvent>());
            }
        }

        [Fact]
        [Category("Exception cases")]
        public async Task CantCancelAnOrderBeforeCreated()
        {
            InMemorySendEvents bus = new InMemorySendEvents();
            PurchaseOrder sut = new PurchaseOrder(bus);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                sut.Handle(new CancelCommand
                {
                    Id = 123
                }));
        }
    }
}
