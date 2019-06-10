using System;
using System.Collections.ObjectModel;
using System.Linq;
using ScanMonitor.Database.DocumentTypeToevoegen;
using ScanMonitor.Database.GetDocumentTypes;
using ScanMonitor.Database.SearchDocuments;

namespace ScanMonitor.UI.Admin
{
    public class DocumentTypeAdminViewModel : GenericAdminViewModel
    {
        public DocumentTypeAdminViewModel()
        {
            var documentTypes = GetDocumentTypesQuery.List()
                .Select(x => new AdminItem { Id = x.Id, Name = x.Name }).ToList();

            Items = new ObservableCollection<AdminItem>(documentTypes);
            OriginalItems = documentTypes;
        }

        public override string Title => "Beheer van document types";

        protected override bool CanBeDeleted(AdminItem item)
        {
            return !SearchDocumentsQuery.List(new SearchDocumentsRequest { DocumentTypeId = (string)item.Id }).Any();
        }

        protected override string Message => "Document type kan niet verwijderd worden, want er zijn nog documenten aanwezig in de databank die van dit type zijn.";

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
            // todo
        }

        protected override void DeleteItem(AdminItem item)
        {
            // todo
        }
    }
}