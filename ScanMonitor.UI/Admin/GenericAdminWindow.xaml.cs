using System.Windows;
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
        }
    }
}
