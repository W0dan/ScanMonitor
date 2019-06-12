using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ScanMonitor.Exceptions;

namespace ScanMonitor.UI.Admin
{
    public class GenericAdminViewModel
    {
        public virtual string Title => "Admin";
        public virtual bool HasEdit => false;

        public ObservableCollection<AdminItem> Items { get; set; }
        protected List<AdminItem> OriginalItems { get; set; }

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

        public void Save()
        {
            Insert();
            Update();
            Delete();
        }

        private void Insert()
        {
            foreach (var item in Items.Where(x => x.Id == null))
                AddItem(item);
        }

        private void Update()
        {
            foreach (var item in Items)
            {
                if (OriginalItems.Any(x => x.Id == item.Id && x.Name != item.Name))
                {
                    UpdateItem(item);
                }
            }
        }

        public virtual void NavigateToEdit(Window owner, AdminItem item) { }

        private void Delete()
        {
            foreach (var originalItem in OriginalItems)
            {
                if (Items.All(x => x.Id != originalItem.Id))
                    DeleteItem(originalItem);
            }
        }

        protected virtual void AddItem(AdminItem item) { }
        protected virtual void UpdateItem(AdminItem item) { }
        protected virtual void DeleteItem(AdminItem item) { }
    }
}