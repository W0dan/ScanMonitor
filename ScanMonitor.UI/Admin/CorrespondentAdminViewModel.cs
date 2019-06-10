using System.Collections.ObjectModel;
using System.Linq;
using ScanMonitor.Database.CorrespondentToevoegen;
using ScanMonitor.Database.GetCorrespondents;
using ScanMonitor.Database.SearchDocuments;

namespace ScanMonitor.UI.Admin
{
    public class CorrespondentAdminViewModel : GenericAdminViewModel
    {
        public CorrespondentAdminViewModel()
        {
            var correspondents = GetCorrespondentsQuery.List()
                .Select(x => new AdminItem { Id = x.Id, Name = x.Name }).ToList();

            Items = new ObservableCollection<AdminItem>(correspondents);
            OriginalItems = correspondents;
        }

        public override string Title => "Beheer van correspondenten";

        protected override bool CanBeDeleted(AdminItem item)
        {
            return !SearchDocumentsQuery.List(new SearchDocumentsRequest { CorrespondentId = (string)item.Id }).Any();
        }

        protected override string Message => "Correspondent kan niet verwijderd worden, want er zijn nog documenten aanwezig in de databank die afkomstig zijn van deze correspondent.";

        protected override void AddItem(AdminItem item)
        {
            CorrespondentToevoegenQuery.Insert(new CorrespondentToevoegenCommand
            {
                Id = (string)item.Id,
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