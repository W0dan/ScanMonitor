namespace ScanMonitor.Database.CustomFieldToevoegen
{
    public class CustomFieldToevoegenCommand
    {
        public string Id { get; set; }
        public string DocumentTypeId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool Mandatory { get; set; }
    }
}