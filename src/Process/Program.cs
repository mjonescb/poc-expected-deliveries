namespace Process
{
    using System;
    using SimpleInjector;
    using Topshelf;
    using Topshelf.SimpleInjector;

    class Program
    {
        static readonly Container Container = new Container();

        static void Main(string[] args)
        {
            TopshelfExitCode rc = HostFactory.Run(x =>
            {
                x.UseSimpleInjector(Container);
                
                x.Service<SimpleTimerService>(s =>
                {
                    s.ConstructUsingSimpleInjector();
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Expected Deliveries service");
                x.SetDisplayName("expected-deliveries");
                x.SetServiceName("expected-deliveries");
            });

            int exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
