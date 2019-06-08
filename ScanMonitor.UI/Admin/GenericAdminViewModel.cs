using System.Collections.ObjectModel;

namespace ScanMonitor.UI.Admin
{
    public class GenericAdminViewModel
    {
        public string Title { get; set; }

        public ObservableCollection<AdminItem> Items { get; set; }

        public void Delete(AdminItem clickedItem)
        {
            throw new System.NotImplementedException();
        }
    }
}