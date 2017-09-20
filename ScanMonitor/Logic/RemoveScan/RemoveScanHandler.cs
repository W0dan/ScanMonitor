using System;
using System.IO;

namespace ScanMonitor.Logic.RemoveScan
{
    public static class RemoveScanHandler
    {
        public static void Handle(RemoveScanCommand command)
        {
            try
            {
                var fi = new FileInfo(command.Filename);

                fi.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}