using System;

namespace ScanMonitor.Database.GetDocumentForEdit
{
    public class CustomFieldDto
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }

        public string StringValue { get; set; }
        public decimal NumericValue { get; set; }
        public bool BooleanValue { get; set; }
        public DateTime DateValue { get; set; }
    }
}