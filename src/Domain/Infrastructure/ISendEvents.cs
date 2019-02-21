namespace Domain.Infrastructure
{
    using System.Threading.Tasks;
    using Bases;

    public interface ISendEvents
    {
        Task SendAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}