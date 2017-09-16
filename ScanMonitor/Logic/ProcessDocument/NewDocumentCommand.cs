using System;

namespace ScanMonitor.Logic.ProcessDocument
{
    public class NewDocumentCommand
    {
        public string Filename { get; set; }
        public Guid DocumentTypeId { get; set; }
        public int PersonId { get; set; }
        public Guid CorrespondentId { get; set; }
        public DateTime Datum { get; set; }
        public string Description { get; set; }
    }
}