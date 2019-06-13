using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ScanMonitor.UI.Admin;

namespace ScanMonitor.UI.Base
{
    public abstract class ListViewModel<T>
        where T : IHasId<string>
    {
        public void Save()
        {
            Insert();
            Update();
            Delete();
        }

        private void Insert()
        {
            foreach (var item in Items.Where(x => string.IsNullOrWhiteSpace(x.Id)))
                AddItem(item);
        }

        public ObservableCollection<T> Items { get; set; }
        protected List<T> OriginalItems { get; set; }

        private void Update()
        {
            foreach (var item in Items.Where(x => OriginalItems.Any(y => IsChanged(x, y))))
            {
                UpdateItem(item);
            }
        }

        private void Delete()
        {
            foreach (var originalItem in OriginalItems)
            {
                if (Items.All(x => x.Id != originalItem.Id))
                    DeleteItem(originalItem);
            }
        }

        protected abstract bool IsChanged(T item, T origItem);

        protected abstract void AddItem(T item);
        protected abstract void UpdateItem(T item);
        protected abstract void DeleteItem(T item);
    }
}