namespace Process.Aspects.Notifications
{
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using MediatR.Pipeline;
    using Pipeline;

    public class NotificationsSender<TRequest, TResponse>
        : IRequestPostProcessor<TRequest, TResponse>
    {
        readonly IMediator mediator;

        public NotificationsSender(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Process(TRequest request, TResponse response)
        {
            CommandResult commandResponse = response as CommandResult;

            if(commandResponse != null)
            {
                await Task.WhenAll(
                    commandResponse
                        .GetNotifications()
                        .Select(x => mediator.Publish(x)));
            }
        }
    }
}
