using ScanMonitor.Database._Interfaces;
using System;

namespace ScanMonitor.Database.GetCustomFields
{
    public class CustomFieldDto : ICustomFieldDto
    {
        public string Id { get; set; }

        public string DocumentTypeCustomFieldId { get; set; }

        public string FieldName { get; set; }
        public string FieldType { get; set; }

        public string StringValue { get; set; }
        public decimal? NumericValue { get; set; }
        public bool? BooleanValue { get; set; }
        public DateTime? DateValue { get; set; }
    }
}
