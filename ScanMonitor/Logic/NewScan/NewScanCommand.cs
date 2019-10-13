using System;
using System.Collections.Generic;
using ScanMonitor.Database.GetCustomFields;

namespace ScanMonitor.Logic.NewScan
{
    public class NewScanCommand
    {
        public Guid? DocumentId { get; set; }

        public string Filename { get; set; }
        public Guid DocumentTypeId { get; set; }
        public string PersonId { get; set; }
        public Guid CorrespondentId { get; set; }
        public DateTime Datum { get; set; }
        public string Description { get; set; }
        public List<CustomFieldDto> CustomFields { get; set; }
    }
}