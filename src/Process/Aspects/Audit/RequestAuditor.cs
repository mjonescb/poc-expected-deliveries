namespace Process.Aspects.Audit
{
    using System.Threading.Tasks;
    using log4net.Core;
    using MediatR.Pipeline;
    using Pipeline;

    public class RequestAuditor<TRequest, TResponse>
        : IRequestPostProcessor<TRequest, TResponse>
    {
        readonly ILogger logger;

        public RequestAuditor(ILogger logger)
        {
            this.logger = logger;
        }

        public Task Process(TRequest request, TResponse response)
        {
            string logMessage = $"Handled {request.GetType().Name}";
            
            if(!(response is CommandResult))
            {
                logMessage += $", result={response.GetType().Name}";
            }

            logger.Log(
                request.GetType(),
                Level.Info,
                logMessage,
                null);

            return Task.CompletedTask;
        }
    }
}
