namespace Process.Pipeline
{
    using System.Collections.Generic;
    using MediatR;

    public class CommandResult
    {
        public static CommandResult Void => new CommandResult();

        CommandResult()
        {
        }

        readonly List<INotification> notifications = new List<INotification>();

        public CommandResult WithNotification(INotification notification)
        {
            notifications.Add(notification);
            return this;
        }

        public IEnumerable<INotification> GetNotifications()
        {
            return notifications.AsReadOnly();
        }
    }
}