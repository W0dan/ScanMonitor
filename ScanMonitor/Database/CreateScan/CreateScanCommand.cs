using System;

namespace ScanMonitor.Database.CreateScan
{
    public class CreateScanCommand
    {
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public string PartialFilename { get; set; }
        public DateTime Datum { get; set; }
    }
}