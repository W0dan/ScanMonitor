using System;
using System.Collections.Generic;

namespace ScanMonitor.Database.GetDocumentById
{
    public class DocumentDto
    {
        public string Id { get; set; }
        public string DocumentType { get; set; }
        public string VoorWie { get; set; }
        public string Correspondent { get; set; }
        public DateTime DatumOntvangen { get; set; }
        public string Beschrijving { get; set; }

        public List<ScanDto> Scans { get; set; }
    }

    public class ScanDto
    {
        public string Id { get; set; }
        public string Filename { get; set; }
        public DateTime Datum { get; set; }
    }
}