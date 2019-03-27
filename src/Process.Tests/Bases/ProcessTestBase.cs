namespace Process.Tests.Bases
{
    using System;
    using Doubles;
    using IoC;
    using MediatR;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;

    public abstract class ProcessTestBase : IDisposable
    {
        protected Func<IMediator> Mediator { get; }

        protected Func<InMemoryNotificationStore> Notifications { get; }

        readonly Scope scope;
        
        protected ProcessTestBase()
        {
            Mediator = () => ContainerFactory.Instance.GetInstance<IMediator>();

            Notifications = () => ContainerFactory.Instance.GetInstance<InMemoryNotificationStore>();

            scope = AsyncScopedLifestyle.BeginScope(ContainerFactory.Instance);
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}