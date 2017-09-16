using System.Configuration;

namespace ScanMonitor
{
    public static class AppSettings
    {
        public static string RootDocumentPath => ConfigurationManager.AppSettings[nameof(RootDocumentPath)];
        public static string ScanPath => ConfigurationManager.AppSettings[nameof(ScanPath)];
    }
}