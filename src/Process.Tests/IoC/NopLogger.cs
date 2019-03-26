namespace Process.Tests.IoC
{
    using System;
    using log4net.Core;
    using log4net.Repository;

    public class NopLogger : ILogger
    {
        public void Log(
            Type callerStackBoundaryDeclaringType,
            Level level,
            object message,
            Exception exception)
        {
        }

        public void Log(LoggingEvent logEvent)
        {
        }

        public bool IsEnabledFor(Level level)
        {
            return false;
        }

        public string Name => "Nop";

        public ILoggerRepository Repository => null;
    }
}