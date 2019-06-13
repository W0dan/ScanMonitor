using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ScanMonitor.UI.Admin;

namespace ScanMonitor.UI.Base
{
    public abstract class ListViewModel<T>
        where T : IHasId<string>
    {
        protected ListViewModel()
        {
        }

        protected ListViewModel(List<T> items)
        {
            Items = new ObservableCollection<T>(items);
            OriginalItems = items;
        }

        public ObservableCollection<T> Items { get; set; }
        public List<T> OriginalItems { get; set; }

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