using System;
using System.Collections.Generic;
using System.Linq;

namespace ScanMonitor.Database.SearchDocuments
{
    public class DocumentDto
    {
        public string Id { get; set; }
        public string DocumentType { get; set; }
        public string VoorWie { get; set; }
        public string Correspondent { get; set; }
        public DateTime DatumOntvangen { get; set; }
        public string Beschrijving { get; set; }
        public string Files { get; set; }

        public List<string> FileList
        {
            get
            {
                if (Files == null || Files.Trim().Length == 0)
                    return new List<string>();
                else
                    return Files.Split(',').ToList();
            }
        }
    }
}