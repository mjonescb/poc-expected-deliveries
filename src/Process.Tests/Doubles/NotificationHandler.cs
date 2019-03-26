namespace Process.Tests.Doubles
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class NotificationHandler<TNotification>
        : INotificationHandler<TNotification>
        where TNotification : INotification
    {
        readonly InMemoryNotificationStore store;

        public NotificationHandler(InMemoryNotificationStore store)
        {
            this.store = store;
        }

        public Task Handle(
            TNotification notification,
            CancellationToken cancellationToken)
        {
            store.Add(notification);
            return Task.CompletedTask;
        }
    }
}
