using System.Windows;
using ScanMonitor.UI.Base;

namespace ScanMonitor.UI.Admin
{
    public class GenericAdminViewModel : ListViewModel<AdminItem>
    {
        public virtual string Title => "Admin";
        public virtual bool HasEdit => false;

        public virtual void NavigateToEdit(Window owner, AdminItem item) { }

        protected override bool IsChanged(AdminItem item, AdminItem origItem)
        {
            return !string.IsNullOrWhiteSpace(item.Id)
                   && item.Name != origItem.Name;
        }

        protected override void AddItem(AdminItem item) { }
        protected override void UpdateItem(AdminItem item) { }
        protected override void DeleteItem(AdminItem item) { }
    }
}