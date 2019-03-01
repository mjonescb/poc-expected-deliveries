namespace Domain.Infrastructure
{
    using System.Threading.Tasks;

    public interface IScheduleCommands
    {
        Task SetCallbackAsync(
            string cron,
            string method, // HTTP verb POST, PUT, PATCH etc.
            string action, // url
            object state = null);
    }
}
