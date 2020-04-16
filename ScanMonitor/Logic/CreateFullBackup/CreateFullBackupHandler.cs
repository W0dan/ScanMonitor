using ScanMonitor.Config;
using ScanMonitor.Extensions;
using System;
using System.IO;
using System.Threading;

namespace ScanMonitor.Logic.CreateFullBackup
{
    public class CreateFullBackupHandler
    {
        public static void CreateBackup(Action<ProgressInfo> reportProgressCallback)
        {
            var sourcePath = AppConfig.AppSettings.RootDocumentPath;
            var backupLocations = AppConfig.AppSettings.BackupLocations;

            var sourceDi = new DirectoryInfo(sourcePath);

            var totalNumberOfSourceFiles = sourceDi.GetTotalNumberOfFiles();
            var currentIndex = 0;

            foreach (var destinationPath in backupLocations)
            {
                var destinationDi = new DirectoryInfo(destinationPath);

                if (destinationDi.Exists)
                {
                    sourceDi.CopyTo(destinationDi, x =>
                    {
                        currentIndex++;
                        reportProgressCallback(new ProgressInfo { Total = totalNumberOfSourceFiles, Current = currentIndex, Text = x.Text });
                    });
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
