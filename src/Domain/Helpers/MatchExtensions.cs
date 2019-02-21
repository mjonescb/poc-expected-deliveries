namespace Domain.Helpers
{
    public static class MatchExtensions
    {
        public static Match Match(this object subject)
        {
            return new Match(subject);
        }
    }
}