using System;

namespace ScanMonitor.UI.Exceptions
{
    public class ScanMonitorException : Exception
    {
        public ScanMonitorException()
        {
        }

        public ScanMonitorException(string message) : base(message)
        {
        }
    }
}