namespace ScanMonitor.Database.CustomFieldAanpassen
{
    public class CustomFieldAanpassenCommand
    {
        public string Id { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool Mandatory { get; set; }
    }
}