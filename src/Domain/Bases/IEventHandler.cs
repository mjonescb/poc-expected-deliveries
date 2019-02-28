namespace Domain.Bases
{
    using System.Threading.Tasks;

    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
