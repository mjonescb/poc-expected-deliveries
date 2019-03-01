namespace Domain.Bases
{
    public class AggregateRootState<TKey>
    {
        public TKey Id { get; set; }
        public int Version { get; set; }

        public void IncrementVersion()
        {
            Version++;
        }
    }
}
