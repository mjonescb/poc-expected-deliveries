namespace Process.Tests.Bases
{
    using System;
    using IoC;
    using MediatR;

    public abstract class ProcessTestBase
    {
        protected Func<IMediator> Mediator { get; }

        protected Func<InMemoryNotificationStore> Notifications { get; }

        protected ProcessTestBase()
        {
            Mediator = () => ContainerFactory.Instance.GetInstance<IMediator>();

            Notifications = () => ContainerFactory.Instance.GetInstance<InMemoryNotificationStore>();
        }
    }
}