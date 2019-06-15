namespace ScanMonitor.UI.Admin.DocumentTypes
{
    public class CustomFieldDto : IHasId<string>, ICanBeCloned<CustomFieldDto>
    {
        public string Id { get; set; }
        public string DocumentTypeId { get; set; }
        public string FieldName { get; set; }
        public FieldTypes FieldType { get; set; }
        public bool Mandatory { get; set; }

        public CustomFieldDto Clone()
        {
            return new CustomFieldDto
            {
                Id = Id,
                FieldType = FieldType,
                FieldName = FieldName,
                Mandatory = Mandatory,
                DocumentTypeId = DocumentTypeId
            };
        }
    }

    public enum FieldTypes
    {
        Tekst,
        Numeriek,
        Datum,
        JaNee,
    }
}