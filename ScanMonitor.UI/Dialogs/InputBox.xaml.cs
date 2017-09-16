using System.Windows;

namespace ScanMonitor.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        private bool _okClicked;

        public InputBox()
        {
            InitializeComponent();
        }

        public static string Show(Window owner, string title, string caption)
        {
            var window = new InputBox
            {
                Title = title,
                CaptionLabel = {Content = caption},
                Owner = owner
            };

            window.ShowDialog();

            return window._okClicked 
                ? window.TextBox.Text 
                : null;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            _okClicked = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
