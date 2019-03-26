namespace Process.Tests.Bases
{
    using System;
    using IoC;
    using MediatR;

    public abstract class ProcessTestBase
    {
        protected Func<IMediator> Mediator;

        protected Func<InMemoryNotificationStore> Notifications;

        protected ProcessTestBase()
        {
            Mediator = () => ContainerFactory.Instance.GetInstance<IMediator>();

            Notifications = () => ContainerFactory.Instance.GetInstance<InMemoryNotificationStore>();
        }
    }
}