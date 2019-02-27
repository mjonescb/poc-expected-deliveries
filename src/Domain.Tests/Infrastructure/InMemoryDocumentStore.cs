namespace Domain.Tests.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bases;
    using Domain.Infrastructure;

    public class InMemoryDocumentStore : IStoreDocuments
    {
        readonly List<AggregateRootState> innerList;

        public InMemoryDocumentStore()
        {
            innerList = new List<AggregateRootState>();
        }

        public Task StoreAsync<TState>(TState state)
            where TState : AggregateRootState
        {
            innerList.Add(state);

            return Task.CompletedTask;
        }
    }
}
