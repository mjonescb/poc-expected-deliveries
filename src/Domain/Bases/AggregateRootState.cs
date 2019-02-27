namespace Domain.Bases
{
    public class AggregateRootState
    {
        public int Version { get; set; }

        public void IncrementVersion()
        {
            Version++;
        }
    }
}
