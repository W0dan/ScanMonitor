using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using ScanMonitor.Database.DocumentTypeToevoegen;
using ScanMonitor.UI.Exceptions;

namespace ScanMonitor.UI.Admin
{
    public class GenericAdminViewModel
    {
        public virtual string Title => "Admin";

        public ObservableCollection<AdminItem> Items { get; set; }
        public List<AdminItem> OriginalItems { get; set; }

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
            // todo: save the collection
            var itemsToAdd = new List<AdminItem>();
            var itemsToUpdate = new List<AdminItem>();
            var itemsToDelete = new List<AdminItem>();

            foreach (var item in Items)
            {
                if (item.Id == null)
                {
                    itemsToAdd.Add(item);
                    continue;
                }
                if (OriginalItems.Any(x => x.Id == item.Id && x.Name != item.Name))
                {
                    itemsToUpdate.Add(item);
                    continue;
                }
            }

            foreach (var item in OriginalItems)
            {
                if (!Items.Any(x => x.Id == item.Id))
                {
                    itemsToDelete.Add(item);
                }
            }

            foreach (var itemToAdd in itemsToAdd)
            {
                AddItem(itemToAdd);
            }
            foreach (var itemToUpdate in itemsToUpdate)
            {
                // todo: update items
                UpdateItem(itemToUpdate);
            }
            foreach (var itemToDelete in itemsToDelete)
            {
                // todo: delete items
                DeleteItem(itemToDelete);
            }
        }

        protected virtual void AddItem(AdminItem item) { }
        protected virtual void UpdateItem(AdminItem item) { }
        protected virtual void DeleteItem(AdminItem item) { }
    }
}