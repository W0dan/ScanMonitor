using System;

namespace ScanMonitor.Logic.NewScan
{
    public class NewScanCommand
    {
        public Guid? DocumentId { get; set; }

        public string Filename { get; set; }
        public Guid DocumentTypeId { get; set; }
        public Guid PersonId { get; set; }
        public Guid CorrespondentId { get; set; }
        public DateTime Datum { get; set; }
        public string Description { get; set; }
    }
}