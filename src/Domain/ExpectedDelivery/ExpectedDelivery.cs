namespace Domain.ExpectedDelivery
{
    using System;
    using System.Threading.Tasks;
    using Bases;
    using Commands;
    using Events;
    using Helpers;
    using Infrastructure;
    using MediatR;
    using Time;

    public class ExpectedDelivery : AggregateRoot<Snapshot, int>
    {
        public ExpectedDelivery(
            IMediator publisher,
            IStoreDocuments documentStore) : base(publisher, documentStore)
        {
            Snapshot = new Snapshot();
        }

        #region Command handlers

        public async Task Handle(CreateCommand command)
        {
            if(Snapshot.State != State.None)
            {
                throw new InvalidOperationException();
            }

            await EmitAsync(new CreatedEvent
            {
                PurchaseOrderLineId = command.PurchaseOrderLineId,
                DeliveryExpected = command.DeliveryDate
            });
        }

        public async Task Handle(CancelCommand command)
        {
            if(Snapshot.State != State.Submitted)
            {
                throw new InvalidOperationException();
            }

            await EmitAsync(new CancelledEvent
            {
                Id = Snapshot.Id
            });
        }

        public async Task Handle(ReviewCommand command)
        {
            if(Snapshot.State != State.Submitted)
            {
                return;
            }

            if(Clock.Current.GetNow() >
               Snapshot.ExpectedDeliveryDate + TimeSpan.FromDays(60))
            {
                await EmitAsync(new CancellationSuggestedEvent());
            }
        }

        #endregion

        protected override Snapshot UpdateState<TEvent>(TEvent @event)
        {
            Snapshot state = Snapshot;
            
            @event
                .Match()
                .When<CreatedEvent>(e =>
                {
                    state.Id = e.PurchaseOrderLineId;
                    state.State = State.Submitted;
                    state.ExpectedDeliveryDate = e.DeliveryExpected;
                })
                .When<CancelledEvent>(e =>
                {
                    state.State = State.Cancelled;
                });

            return state;
        }
    }
}
