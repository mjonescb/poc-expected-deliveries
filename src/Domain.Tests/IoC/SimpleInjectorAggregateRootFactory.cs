namespace Domain.Tests.IoC
{
    using System.Threading.Tasks;
    using Bases;
    using Domain.Infrastructure;
    using SimpleInjector;

    public class SimpleInjectorAggregateRootFactory : IAggregateRootFactory
    {
        readonly Container container;
        readonly IStoreDocuments documentStore;

        public SimpleInjectorAggregateRootFactory(
            Container container,
            IStoreDocuments documentStore)
        {
            this.container = container;
            this.documentStore = documentStore;
        }

        public async Task<TAggregate> LoadAsync<TAggregate, TState, TKey>(TKey id)
            where TAggregate : AggregateRoot<TState, TKey>
            where TState : AggregateRootState<TKey>
        {
            TAggregate result = container.GetInstance<TAggregate>();
            TState state = await documentStore.GetAsync<TState, TKey>(id);

            result.Load(state);
            return result;
        }
    }
}
