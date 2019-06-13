using ScanMonitor.UI.Base;

namespace ScanMonitor.UI.Admin.DocumentTypes
{
    public class DocumentTypeDetailAdminViewModel : ListViewModel<CustomFieldDto>
    {
        public string Title { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }

        protected override bool IsChanged(CustomFieldDto item, CustomFieldDto origItem)
        {
            return !string.IsNullOrWhiteSpace(item.Id)
                   && (item.FieldName != origItem.FieldName
                        || item.FieldType != origItem.FieldType
                        || item.Mandatory != origItem.Mandatory);
        }

        protected override void AddItem(CustomFieldDto item)
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateItem(CustomFieldDto item)
        {
            throw new System.NotImplementedException();
        }

        protected override void DeleteItem(CustomFieldDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}