using System;
using System.Collections.ObjectModel;
using System.Linq;
using ScanMonitor.Database.CorrespondentAanpassen;
using ScanMonitor.Database.CorrespondentToevoegen;
using ScanMonitor.Database.CorrespondentVerwijderen;
using ScanMonitor.Database.GetCorrespondents;
using ScanMonitor.Database.SearchDocuments;

namespace ScanMonitor.UI.Admin.Correspondents
{
    public class CorrespondentAdminViewModel : GenericAdminViewModel
    {
        public CorrespondentAdminViewModel() //: base(GetCorrespondentsQuery.List().Select(x => new AdminItem { Id = x.Id, Name = x.Name }).ToList())
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

        protected override string CannotDeleteMessage => "Correspondent kan niet verwijderd worden, want er zijn nog documenten aanwezig in de databank die afkomstig zijn van deze correspondent.";

        protected override void AddItem(AdminItem item)
        {
            CorrespondentToevoegenQuery.Insert(new CorrespondentToevoegenCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = item.Name
            });
        }

        protected override void UpdateItem(AdminItem item)
        {
            CorrespondentAanpassenQuery.Update(new CorrespondentAanpassenCommand
            {
                Id = item.Id,
                Name = item.Name
            });
        }

        protected override void DeleteItem(AdminItem item)
        {
            CorrespondentVerwijderenQuery.Delete(new CorrespondentVerwijderenCommand
            {
                Id = item.Id
            });
        }
    }
}