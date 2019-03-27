namespace Process.Pipeline
{
    using MediatR;

    public abstract class Command : IRequest<CommandResult>
    {
    }
}
