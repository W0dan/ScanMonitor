using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ScanMonitor.Exceptions;

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
            Insert();
            Update();
            Delete();

            //var itemsToAdd = new List<AdminItem>();
            //var itemsToUpdate = new List<AdminItem>();
            //var itemsToDelete = new List<AdminItem>();

            //foreach (var item in Items)
            //{
            //    if (item.Id == null)
            //    {
            //        itemsToAdd.Add(item);
            //        continue;
            //    }
            //}
            //foreach (var item in Items)
            //{
            //    if (OriginalItems.Any(x => x.Id == item.Id && x.Name != item.Name))
            //    {
            //        itemsToUpdate.Add(item);
            //        continue;
            //    }
            //}

            //foreach (var item in OriginalItems)
            //{
            //    if (!Items.Any(x => x.Id == item.Id))
            //    {
            //        itemsToDelete.Add(item);
            //    }
            //}

            //foreach (var itemToAdd in itemsToAdd)
            //{
            //    AddItem(itemToAdd);
            //}
            //foreach (var itemToUpdate in itemsToUpdate)
            //{
            //    UpdateItem(itemToUpdate);
            //}
            //foreach (var itemToDelete in itemsToDelete)
            //{
            //    DeleteItem(itemToDelete);
            //}
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

        private void Delete()
        {
            foreach (var item in OriginalItems)
            {
                if (!Items.Any(x => x.Id == item.Id))
                {
                    DeleteItem(item);
                }
            }
        }

        protected virtual void AddItem(AdminItem item) { }
        protected virtual void UpdateItem(AdminItem item) { }
        protected virtual void DeleteItem(AdminItem item) { }
    }
}