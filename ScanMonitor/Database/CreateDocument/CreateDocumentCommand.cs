using System;

namespace ScanMonitor.Database.CreateDocument
{
    public class CreateDocumentCommand
    {
        public string Id { get; set; }
        public string DocumentTypeId { get; set; }
        public string PersonId { get; set; }
        public string CorrespondentId { get; set; }
        public DateTime Datum { get; set; }
        public string Description { get; set; }
    }
}