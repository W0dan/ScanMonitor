using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ScanMonitor.Config;

namespace ScanMonitor.UI
{
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _worker.DoWork += WaitForFiles;

            _worker.RunWorkerAsync();
        }

        private void WaitForFiles(object sender, DoWorkEventArgs e)
        {
            var watcher = ScannedFilesWatcher.StartWatching(AppConfig.AppSettings.ScanPath);

            foreach (var fileName in watcher.GetNextFile())
            {
                Dispatcher.Invoke(() =>
                {
                    var label = new Label { Content = fileName };
                    ProcessedFiles.Children.Add(label);

                    IndexWindow.ProcessFile(this, fileName);
                });
            }
        }

        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SearchMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            SearchWindow.StartSearch(this);
        }
    }
}
