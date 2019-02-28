namespace Domain.Tests.IoC
{
    using Bases;
    using Domain.Infrastructure;
    using SimpleInjector;

    public class SimpleInjectorAggregateRootFactory : IAggregateRootFactory
    {
        readonly Container container;

        public SimpleInjectorAggregateRootFactory(Container container)
        {
            this.container = container;
        }

        public TAggregate Create<TAggregate, TState>()
            where TAggregate : AggregateRoot<TState>
            where TState : AggregateRootState
        {
            return container.GetInstance<TAggregate>();
        }
    }
}
