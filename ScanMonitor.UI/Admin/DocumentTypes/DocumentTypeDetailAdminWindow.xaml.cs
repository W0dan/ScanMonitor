﻿using System.Windows;
using System.Windows.Input;

namespace ScanMonitor.UI.Admin.DocumentTypes
{
    /// <summary>
    /// Interaction logic for DocumentTypeAdminWindow.xaml
    /// </summary>
    public partial class DocumentTypeDetailAdminWindow : Window
    {
        public DocumentTypeDetailAdminWindow()
        {
            InitializeComponent();
        }

        private static DocumentTypeDetailAdminViewModel Model { get; set; }

        public static void ShowAdmin(Window owner, DocumentTypeDetailAdminViewModel model)
        {
            Model = model;
            var window = new DocumentTypeDetailAdminWindow
            {
                Title = model.Title,
                Owner = owner,
                DataContext = model
            };

            window.ShowDialog();
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            Model.Save();

            MessageBox.Show("Wijzigingen bewaard", "Wijzigingen bewaard", MessageBoxButton.OK);
        }

        private void OnDeleteClicked(object sender, MouseButtonEventArgs e)
        {
            var clickedItem = ((FrameworkElement)sender).DataContext as CustomFieldDto;

            Model.Delete(clickedItem);
        }
    }
}
