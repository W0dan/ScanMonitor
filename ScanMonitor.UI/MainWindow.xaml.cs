using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ScanMonitor.Config;
using ScanMonitor.UI.Admin;
using ScanMonitor.UI.Admin.Correspondents;
using ScanMonitor.UI.Admin.DocumentTypes;
using ScanMonitor.UI.Admin.Users;

namespace ScanMonitor.UI
{
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppConfig.AppSettings.BackupOnStartup)
                BackupWindow.StartBackup(this);

            _worker.DoWork += WaitForFiles;

            _worker.RunWorkerAsync();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (AppConfig.AppSettings.BackupOnClosing)
                BackupWindow.StartBackup(this);
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

                    Indexing.IndexWindow.ProcessFile(this, fileName);
                });
            }
        }

        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SearchMenuItem_OnClick(object sender, RoutedEventArgs e)
            => Searching.SearchWindow.StartSearch(this);

        private void AdminUsersMenuItem_OnClick(object sender, RoutedEventArgs e)
            => GenericAdminWindow.ShowAdmin(this, new UserAdminViewModel());

        private void AdminDocumentTypesMenuItem_OnClick(object sender, RoutedEventArgs e)
            => GenericAdminWindow.ShowAdmin(this, new DocumentTypeAdminViewModel());

        private void AdminCorrespondentsMenuItem_OnClick(object sender, RoutedEventArgs e)
            => GenericAdminWindow.ShowAdmin(this, new CorrespondentAdminViewModel());

        private void BackupMenuItem_Click(object sender, RoutedEventArgs e)
            => BackupWindow.StartBackup(this);
    }
}
