﻿using System.Windows;
using System.Windows.Input;

namespace ScanMonitor.UI.Admin
{
    /// <summary>
    /// Interaction logic for GenericAdminWindow.xaml
    /// </summary>
    public partial class GenericAdminWindow : Window
    {
        public GenericAdminWindow()
        {
            InitializeComponent();
        }

        private GenericAdminViewModel Model => (GenericAdminViewModel)DataContext;

        public static void ShowAdmin(Window owner, GenericAdminViewModel model)
        {
            var window = new GenericAdminWindow
            {
                Title = model.Title,
                Owner = owner,
                DataContext = model
            };

            if (!model.HasEdit)
            {
                window.ItemsGrid.Columns[index: 1].Visibility = Visibility.Hidden;
            }

            window.ShowDialog();
        }

        private void OnDeleteClicked(object sender, MouseButtonEventArgs e)
        {
            var clickedItem = ((FrameworkElement)sender).DataContext as AdminItem;

            Model.Delete(clickedItem);
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            Model.Save();

            MessageBox.Show("Wijzigingen bewaard", "Wijzigingen bewaard", MessageBoxButton.OK);
        }

        private void OnEditClicked(object sender, MouseButtonEventArgs e)
        {
            // -> new window : edit details of admin item  (specific)
            var clickedItem = ((FrameworkElement)sender).DataContext as AdminItem;

            Model.NavigateToEdit(this, clickedItem);
        }
    }
}
