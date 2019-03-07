namespace Process
{
    using System.Timers;

    public class SimpleTimerService
    {
        readonly Timer timer;
        
        public SimpleTimerService()
        {
            timer = new Timer(1000)
            {
                AutoReset = true
            };

            timer.Elapsed += (sender, eventArgs) => { };
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        } 
    }
}
