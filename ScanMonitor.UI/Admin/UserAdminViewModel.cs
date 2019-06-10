using System.Collections.ObjectModel;
using System.Linq;
using ScanMonitor.Database.GetPeople;
using ScanMonitor.Database.SearchDocuments;

namespace ScanMonitor.UI.Admin
{
    public class UserAdminViewModel : GenericAdminViewModel
    {
        public UserAdminViewModel()
        {
            var people = GetPeopleQuery.List()
                .Select(x => new AdminItem { Id = x.Id, Name = x.Name }).ToList();

            Items = new ObservableCollection<AdminItem>(people);
            OriginalItems = people;
        }

        public override string Title => "Beheer van gebruikers";

        protected override bool CanBeDeleted(AdminItem item)
        {
            return !SearchDocumentsQuery.List(new SearchDocumentsRequest { PersonId = (int)item.Id }).Any();
        }

        protected override string Message => "Gebruiker kan niet verwijderd worden, want er zijn nog documenten aanwezig in de databank die behoren to deze gebruiker.";

        protected override void AddItem(AdminItem item)
        {
            // todo
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