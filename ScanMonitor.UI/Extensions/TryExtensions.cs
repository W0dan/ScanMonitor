using System;
using System.Threading;

namespace ScanMonitor.UI.Extensions
{
    public class TryExtensions
    {
        private readonly Action _action;

        private TryExtensions(Action action)
        {
            _action = action;
        }

        public static TryExtensions Try(Action action)
        {
            return new TryExtensions(action);
        }

        public void Times(int count)
        {
            var timesTried = 0;

            Console.WriteLine("trying the action for the first time");

            while (timesTried < count)
            {
                try
                {
                    _action();
                    return;
                }
                catch (Exception)
                {
                    timesTried++;

                    Console.WriteLine("exception occured, retrying after 200ms");
                    Thread.Sleep(200);
                }
            }
        }
    }
}