using System.Collections.Generic;

namespace ScanMonitor.Database.GetDocumentTypeById
{
    public class DocumentTypeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<CustomFieldDto> CustomFields { get; set; }
    }
}