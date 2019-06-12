using System.Collections.Generic;

namespace ScanMonitor.UI.Admin.DocumentTypes
{
    public class DocumentTypeDetailAdminViewModel
    {
        public string Title { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }

        public List<CustomFieldDto> CustomFields { get; set; }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}