using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ScanMonitor.Exceptions;
using ScanMonitor.UI.Admin;

namespace ScanMonitor.UI.Base
{
    public abstract class ListViewModel<T>
        where T : IHasId<string>, ICanBeCloned<T>
    {
        private ObservableCollection<T> items;

        public ObservableCollection<T> Items
        {
            get => items;
            set
            {
                items = value;
                OriginalItems = items.Select(x => x.Clone()).ToList();
            }
        }

        private List<T> OriginalItems { get; set; }

        public void Delete(T item)
        {
            if (item == null)
                throw new ScanMonitorException("Dit item kan niet verwijderd worden.");

            if (CanBeDeleted(item))
                Items.Remove(item);
            else
                throw new ScanMonitorException(CannotDeleteMessage);
        }

        protected virtual bool CanBeDeleted(T item)
        {
            return true;
        }

        protected virtual string CannotDeleteMessage => "";

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