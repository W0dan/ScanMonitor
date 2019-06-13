namespace ScanMonitor.UI.Admin.DocumentTypes
{
    public class CustomFieldDto : IHasId<string>
    {
        public string Id { get; set; }
        public string DocumentTypeId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool Mandatory { get; set; }
    }
}