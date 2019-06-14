using System;
using System.Collections.Generic;
using ScanMonitor.Database.CustomFieldAanpassen;
using ScanMonitor.Database.CustomFieldToevoegen;
using ScanMonitor.Database.CustomFieldVerwijderen;
using ScanMonitor.UI.Base;

namespace ScanMonitor.UI.Admin.DocumentTypes
{
    public class DocumentTypeDetailAdminViewModel : ListViewModel<CustomFieldDto>
    {
        public string Title { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }

        public List<FieldTypes> DataTypes => new List<FieldTypes>
        {
            FieldTypes.Tekst,
            FieldTypes.Numeriek,
            FieldTypes.Datum,
            FieldTypes.JaNee,
        };

        protected override bool IsChanged(CustomFieldDto item, CustomFieldDto origItem)
        {
            return !string.IsNullOrWhiteSpace(item.Id)
                   && (item.FieldName != origItem.FieldName
                        || item.FieldType != origItem.FieldType
                        || item.Mandatory != origItem.Mandatory);
        }

        protected override void AddItem(CustomFieldDto item)
        {
            CustomFieldToevoegenQuery.Insert(new CustomFieldToevoegenCommand
            {
                Id = Guid.NewGuid().ToString(),
                FieldName = item.FieldName,
                DocumentTypeId = Id,
                FieldType = item.FieldType.ToString(),
                Mandatory = item.Mandatory
            });
        }

        protected override void UpdateItem(CustomFieldDto item)
        {
            CustomFieldAanpassenQuery.Update(new CustomFieldAanpassenCommand
            {
                Id = item.Id,
                FieldName = item.FieldName,
                FieldType = item.FieldType.ToString(),
                Mandatory = item.Mandatory
            });
        }

        protected override void DeleteItem(CustomFieldDto item)
        {
            CustomFieldVerwijderenQuery.Delete(new CustomFieldVerwijderenCommand { Id = item.Id });
        }
    }
}