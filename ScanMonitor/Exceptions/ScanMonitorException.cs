using System;

namespace ScanMonitor.Exceptions
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