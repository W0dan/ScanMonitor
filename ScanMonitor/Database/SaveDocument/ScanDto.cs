using System;

namespace ScanMonitor.Database.SaveDocument
{
    public class ScanDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public DateTime Datum { get; set; }
    }
}