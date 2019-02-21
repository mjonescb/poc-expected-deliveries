namespace Domain.Tests.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bases;
    using Domain.Infrastructure;

    public class InMemorySendEvents : ISendEvents
    {
        readonly List<IEvent> raisedEvents = new List<IEvent>();
        
        public bool WasRaised<TEvent>() where TEvent : IEvent
        {
            return raisedEvents.Any(x => x.GetType() == typeof(TEvent));
        }

        public bool WasNotRaised<TEvent>() where TEvent : IEvent
        {
            return !WasRaised<TEvent>();
        }
        
        public Task SendAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            raisedEvents.Add(@event);
            return Task.CompletedTask;
        }
    }
}