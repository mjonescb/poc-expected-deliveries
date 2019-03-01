namespace Domain.Tests.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class InMemoryEventBus : IMediator
    {
        readonly List<object> raisedEvents = new List<object>();
        
        public bool WasRaised<TEvent>() where TEvent : INotification
        {
            return raisedEvents.Any(x => x.GetType() == typeof(TEvent));
        }

        public bool WasNotRaised<TEvent>() where TEvent : INotification
        {
            return !WasRaised<TEvent>();
        }
        
        public Task SendAsync<TEvent>(TEvent @event) where TEvent : INotification
        {
            raisedEvents.Add(@event);
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task Publish(
            object notification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            raisedEvents.Add(notification);
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(
            TNotification notification,
            CancellationToken cancellationToken = new CancellationToken())
            where TNotification : INotification
        {
            raisedEvents.Add(notification);
            return Task.CompletedTask;
        }
    }
}
