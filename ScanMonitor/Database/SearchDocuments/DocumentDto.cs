using System;

namespace ScanMonitor.Database.SearchDocuments
{
    public class DocumentDto
    {
        public string DocumentType { get; set; }
        public string VoorWie { get; set; }
        public string Correspondent { get; set; }
        public DateTime DatumOntvangen { get; set; }
        public string Beschrijving { get; set; }
        public string FileName { get; set; }
    }
}