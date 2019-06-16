using System;
using System.Collections.Generic;

namespace ScanMonitor.Database.GetDocumentForEdit
{
    public class DocumentDto
    {
        public string Id { get; set; }
        public string DocumentType { get; set; }
        public string PersonId { get; set; }
        public string CorrespondentId { get; set; }
        public DateTime DatumOntvangen { get; set; }
        public string Beschrijving { get; set; }

        public List<ScanDto> Scans { get; set; }
        public List<CustomFieldDto> CustomFields { get; set; }
    }
}