using System.Configuration;

namespace ScanMonitor.Config
{
    public class AppSettings
    {
        public string RootDocumentPath => ConfigurationManager.AppSettings[nameof(RootDocumentPath)];
        public string ScanPath => ConfigurationManager.AppSettings[nameof(ScanPath)];
    }
}