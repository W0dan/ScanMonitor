using System;
using ScanMonitor.Database;

namespace ScanMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfw = ScannedFilesWatcher.StartWatching(@"H:\SCANS");




            Console.ReadLine();
        }
    }
}
