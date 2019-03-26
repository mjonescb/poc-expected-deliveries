namespace Process.Tests.IoC
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class InMemoryNotificationStore
    {
        readonly List<INotification> innerList = new List<INotification>();

        public void Add(INotification notification)
        {
            innerList.Add(notification);
        }

        public bool WasReceived<T>()
        {
            return innerList.OfType<T>().Any();
        }
    }
    
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
