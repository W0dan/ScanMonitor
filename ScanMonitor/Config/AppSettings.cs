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
        public bool BackupOnStartup
        {
            get
            {
                var backupOnStartupString = ConfigurationManager.AppSettings[nameof(BackupOnStartup)];

                return backupOnStartupString != "0";
            }
        }
        public bool BackupOnClosing
        {
            get
            {
                var backupOnClosingString = ConfigurationManager.AppSettings[nameof(BackupOnClosing)];

                return backupOnClosingString != "0";
            }
        }
        public string RootDocumentPath => ConfigurationManager.AppSettings[nameof(RootDocumentPath)];
        public string ScanPath => ConfigurationManager.AppSettings[nameof(ScanPath)];
    }
}