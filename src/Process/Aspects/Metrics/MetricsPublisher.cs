namespace Process.Aspects.Metrics
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class MetricsPublisher<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = await next();
            return response;
        }
    }
}
