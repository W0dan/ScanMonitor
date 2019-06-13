using System.Collections.Generic;
using System.Windows;
using ScanMonitor.Exceptions;
using ScanMonitor.UI.Base;

namespace ScanMonitor.UI.Admin
{
    public class GenericAdminViewModel : ListViewModel<AdminItem>
    {
        public virtual string Title => "Admin";
        public virtual bool HasEdit => false;

        public GenericAdminViewModel(List<AdminItem> items) : base(items) { }

        public void Delete(AdminItem item)
        {
            if (item == null)
                throw new ScanMonitorException("Dit item kan niet verwijderd worden.");

            if (CanBeDeleted(item))
                Items.Remove(item);
            else
            {
                throw new ScanMonitorException(Message);
            }
        }

        protected virtual bool CanBeDeleted(AdminItem item)
        {
            return true;
        }

        protected virtual string Message => "";

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