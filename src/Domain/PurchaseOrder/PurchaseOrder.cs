﻿namespace Domain.PurchaseOrder
{
    using System;
    using System.Threading.Tasks;
    using Bases;
    using Commands;
    using Events;
    using Helpers;
    using Infrastructure;
    using Time;

    public class PurchaseOrder : AggregateRoot<Snapshot>
    {
        int id = 0;

        public PurchaseOrder(ISendEvents publisher) : base(publisher)
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
                Id = command.Id,
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
                Id = id
            });
        }

        public async Task Handle(CheckCommand command)
        {
            if(Snapshot.State != State.Submitted)
            {
                return;
            }

            if(Clock.Current.GetNow() > Snapshot.ExpectedDeliveryDate + TimeSpan.FromDays(60))
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
                    id = e.Id;
                    
                    state = state
                        .ChangeState(State.Submitted)
                        .SetExpectedDelivery(e.DeliveryExpected);
                })
                .When<CancelledEvent>(e =>
                {
                    state = state.ChangeState(State.Cancelled);
                });

            return state;
        }
    }
}