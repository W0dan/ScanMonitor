using System;
using System.Threading;

namespace ScanMonitor.UI.Extensions
{
    public class TryExtensions
    {
        private readonly Action action;
        private int timesTried;
        private Exception exception;

        private TryExtensions(Action action)
        {
            this.action = action;
        }

        public static TryExtensions Try(Action action)
        {
            return new TryExtensions(action);
        }

        public TryExtensions Times(int count)
        {
            timesTried = 0;
            exception = null;

            Console.WriteLine("trying the action for the first time");

            while (timesTried < count)
            {
                try
                {
                    action();
                    return this;
                }
                catch (Exception ex)
                {
                    timesTried++;
                    exception = ex;

                    Console.WriteLine("exception occured, retrying after 200ms");
                    Thread.Sleep(200);
                }
            }

            return this;
        }

        public TryExtensions OnSuccess(Action<int> successAction)
        {
            if (exception == null)
            {
                successAction(timesTried);
            }
            return this;
        }

        public TryExtensions OnFailure(Action<Exception, int> failureAction)
        {
            if (exception != null)
            {
                failureAction(exception, timesTried);
            }
            return this;
        }
    }
}