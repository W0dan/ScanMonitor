using ScanMonitor.Config;
using ScanMonitor.Extensions;
using System.IO;

namespace ScanMonitor.Logic.CreateFullBackup
{
    public class CreateFullBackupHandler
    {
        public static void CreateBackup()
        {
            var sourcePath = AppConfig.AppSettings.RootDocumentPath;
            var backupLocations = AppConfig.AppSettings.BackupLocations;

            foreach (var destinationPath in backupLocations)
            {
                var sourceDi = new DirectoryInfo(sourcePath);
                var destinationDi = new DirectoryInfo(destinationPath);

                if (destinationDi.Exists)
                {
                    sourceDi.CopyTo(destinationDi);
                }
            }
        }
    }
}
