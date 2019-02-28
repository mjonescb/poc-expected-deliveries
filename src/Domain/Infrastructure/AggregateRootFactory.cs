namespace Domain.Infrastructure
{
    using Bases;

    public interface IAggregateRootFactory
    {
        TAggregate Create<TAggregate, TState>()
            where TAggregate : AggregateRoot<TState>
            where TState : AggregateRootState;
    }
}
