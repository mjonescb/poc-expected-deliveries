namespace Process.Aspects.Audit
{
    using System.Threading.Tasks;
    using MediatR.Pipeline;
    using Pipeline;
    using Serilog;

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
            if(response is CommandResult)
            {
                logger.Information(
                    "Handled command {commandName}",
                    request.GetType().Name);
            }
            else
            {
                logger.Information(
                    "Handled query {queryName}, result was {result}",
                    request.GetType().Name,
                    response.GetType().Name);
            }

            return Task.CompletedTask;
        }
    }
}
