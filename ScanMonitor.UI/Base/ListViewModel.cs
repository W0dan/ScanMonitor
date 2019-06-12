using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ScanMonitor.UI.Base
{
    public abstract class ListViewModel<T>
    {

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

        protected IEnumerable<T> Items { get; set; }
        protected IEnumerable<T> OriginalItems { get; set; }

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


        public virtual void NavigateToEdit(Window owner, T item) { }

        private void Delete()
        {
            foreach (var originalItem in OriginalItems)
            {
                if (Items.All(x => x.Id != originalItem.Id))
                    DeleteItem(originalItem);
            }
        }

        protected virtual void AddItem(T item) { }
        protected virtual void UpdateItem(T item) { }
        protected virtual void DeleteItem(T item) { }
    }
}