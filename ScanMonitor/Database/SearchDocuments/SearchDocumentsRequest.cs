using System;

namespace ScanMonitor.Database.SearchDocuments
{
    public class SearchDocumentsRequest
    {
        public string DocumentTypeId { get; set; }
        public string PersonId { get; set; }
        public string CorrespondentId { get; set; }
        public DateTime? Datum { get; set; }
        public string SearchString { get; set; }
    }
}