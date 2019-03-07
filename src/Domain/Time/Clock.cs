namespace Domain.Time
{
    using System;
    using System.Threading;

    public class Clock
    {
        static AsyncLocal<Clock> CurrentValue { get; }

        public static Clock Current => CurrentValue.Value;

        static Clock()
        {
            CurrentValue = new AsyncLocal<Clock>() { Value = new Clock() };
        }
        
        readonly TimeSpan offset;

        Clock(TimeSpan offset)
        {
            this.offset = offset;
        }

        Clock() : this(TimeSpan.Zero)
        {
        }

        public DateTime GetNow() => DateTime.UtcNow + offset;

        public static IDisposable Adjust(TimeSpan offset)
        {
            CurrentValue.Value = new Clock(offset);
            return new Disposer();
        }

        class Disposer : IDisposable
        {
            public void Dispose()
            {
                CurrentValue.Value = new Clock();
            }
        }
    }
}
