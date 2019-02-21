namespace Domain.Helpers
{
    using System;

    public class Match
    {
        readonly object subject;
        readonly bool wasHandled;

        internal Match(object subject)
            : this(subject, false)
        {
        }

        Match(object subject, bool wasHandled)
        {
            this.subject = subject;
            this.wasHandled = wasHandled;
        }

        public Match When<T>(Action<T> handler)
        {
            if (!wasHandled && typeof(T) == subject.GetType())
            {
                handler((T)subject);
                return new Match(subject, true);
            }

            return this;
        }

        public void Else<T>(Action<T> handler)
        {
            if (!wasHandled)
                handler((T) subject);
        }
    }
}