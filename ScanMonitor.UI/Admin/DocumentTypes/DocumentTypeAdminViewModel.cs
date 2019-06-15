using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ScanMonitor.Database.DocumentTypeAanpassen;
using ScanMonitor.Database.DocumentTypeToevoegen;
using ScanMonitor.Database.DocumentTypeVerwijderen;
using ScanMonitor.Database.GetDocumentTypeById;
using ScanMonitor.Database.GetDocumentTypes;
using ScanMonitor.Database.SearchDocuments;
using ScanMonitor.UI.Extensions;

namespace ScanMonitor.UI.Admin.DocumentTypes
{
    public class DocumentTypeAdminViewModel : GenericAdminViewModel
    {
        public DocumentTypeAdminViewModel() //: base(GetDocumentTypesQuery.List().Select(x => new AdminItem { Id = x.Id, Name = x.Name }).ToList())
        {
            var documentTypes = GetDocumentTypesQuery.List()
                .Select(x => new AdminItem { Id = x.Id, Name = x.Name }).ToList();

            Items = new ObservableCollection<AdminItem>(documentTypes);
            //OriginalItems = documentTypes;
        }

        public override string Title => "Beheer van document types";
        public override bool HasEdit => true;

        protected override bool CanBeDeleted(AdminItem item)
        {
            return !SearchDocumentsQuery.List(new SearchDocumentsRequest { DocumentTypeId = (string)item.Id }).Any();
        }

        protected override string CannotDeleteMessage => "Document type kan niet verwijderd worden, want er zijn nog documenten aanwezig in de databank die van dit type zijn.";

        public override void NavigateToEdit(Window owner, AdminItem item)
        {
            var documentType = GetDocumentTypeByIdQuery.Get(new GetDocumentTypeByIdRequest { Id = item.Id });

            var customFields = documentType.CustomFields.ConvertAll(x => new CustomFieldDto { Id = x.Id, FieldType = x.FieldType.ToEnum<FieldTypes>(), FieldName = x.FieldName, Mandatory = x.Mandatory, DocumentTypeId = x.DocumentTypeId });
            DocumentTypeDetailAdminWindow.ShowAdmin(owner, new DocumentTypeDetailAdminViewModel
            {
                Id = documentType.Id,
                Name = documentType.Name,
                Title = $"Document type {documentType.Name} beheren",
                Items = new ObservableCollection<CustomFieldDto>(customFields),
                //OriginalItems = customFields
            });
        }

        protected override void AddItem(AdminItem item)
        {
            DocumentTypeToevoegenQuery.Insert(new DocumentTypeToevoegenCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = item.Name
            });
        }

        protected override void UpdateItem(AdminItem item)
        {
            DocumentTypeAanpassenQuery.Update(new DocumentTypeAanpassenCommand
            {
                Id = item.Id,
                Name = item.Name
            });
        }

        protected override void DeleteItem(AdminItem item)
        {
            DocumentTypeVerwijderenQuery.Delete(new DocumentTypeVerwijderenCommand
            {
                Id = item.Id
            });
        }
    }
}