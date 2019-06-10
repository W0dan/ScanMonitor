using System.Windows;
using System.Windows.Threading;
using ScanMonitor.UI.Exceptions;

namespace ScanMonitor.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is ScanMonitorException)
            {
                MessageBox.Show($"{e.Exception.Message}", "ScanMonitor", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show($"An unhandled exception just occured: {e.Exception.Message}", "ScanMonitor",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            e.Handled = true;
        }
    }
}
