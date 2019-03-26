namespace Process.Tests.IoC
{
    using System.Collections.Generic;
    using System.Linq;
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
}