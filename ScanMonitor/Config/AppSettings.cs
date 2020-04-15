using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ScanMonitor.Config
{
    public class AppSettings
    {
        public List<string> BackupLocations
        {
            get
            {
                var backupLocationsString = ConfigurationManager.AppSettings[nameof(BackupLocations)];
                var backupLocations = backupLocationsString.Split(';');

                return backupLocations.ToList();
            }
        }
        public string RootDocumentPath => ConfigurationManager.AppSettings[nameof(RootDocumentPath)];
        public string ScanPath => ConfigurationManager.AppSettings[nameof(ScanPath)];
    }
}