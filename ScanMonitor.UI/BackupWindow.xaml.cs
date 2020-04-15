using ScanMonitor.Logic.CreateFullBackup;
using System;
using System.ComponentModel;
using System.Windows;

namespace ScanMonitor.UI
{
    /// <summary>
    /// Interaction logic for BackupWindow.xaml
    /// </summary>
    public partial class BackupWindow : Window
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public BackupWindow()
        {
            InitializeComponent();
        }

        public static void StartBackup(Window owner)
        {
            var window = new BackupWindow
            {
                Title = "Backup in progress ...",
                Owner = owner
            };

            window.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _worker.DoWork += PerformBackup;

            _worker.RunWorkerAsync();
        }

        private void PerformBackup(object sender, DoWorkEventArgs e)
        {
            CreateFullBackupHandler.CreateBackup(UpdateProgress);

            Dispatcher.Invoke(() => this.Close());
        }

        private void UpdateProgress(ProgressInfo progressInfo)
        {
            Dispatcher.Invoke(() =>
            {
                this.BackupProgress.Maximum = progressInfo.Total;
                this.BackupProgress.Value = progressInfo.Current;
                this.BackupListBox.Items.Insert(0, progressInfo.Text);
            });
        }
    }
}
