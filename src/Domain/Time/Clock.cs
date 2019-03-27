namespace Domain.Time
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    public class Clock
    {
        readonly TimeSpan offset;

        static AsyncLocal<Clock> currentValue;

        public static Clock Instance
        {
            get
            {
                if(currentValue == null)
                {
                    currentValue = new AsyncLocal<Clock> { Value = new Clock() };
                }

                if(currentValue.Value == null)
                {
                    currentValue.Value = new Clock();
                }

                return currentValue.Value;
            }
        }

        static Clock()
        {
            currentValue = new AsyncLocal<Clock>
            {
                Value = new Clock()
            };
        }

        public DateTime Now => DateTime.Now + offset;
        
        public Clock() : this(TimeSpan.Zero)
        {
        }

        public Clock(TimeSpan offset)
        {
            this.offset = offset;
        }

        public IDisposable Adjust(TimeSpan newOffset)
        {
            Trace.WriteLine("Adjust");
            currentValue.Value = new Clock(newOffset);
            Trace.WriteLine("Adjust end");
            return new Disposer();
        }

        public override string ToString()
        {
            if(offset == TimeSpan.Zero)
            {
                return "No offset";
            }

            Trace.WriteLine("Offset = " + offset.TotalSeconds);
            return offset.ToString();
        }

        public class Disposer : IDisposable
        {
            public void Dispose()
            {
                Trace.WriteLine("Dispose");
                currentValue.Value = new Clock();
                Trace.WriteLine("Dispose end");
            }
        }
    }
}
