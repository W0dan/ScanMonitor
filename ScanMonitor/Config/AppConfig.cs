namespace ScanMonitor.Config
{
    public static class AppConfig
    {
        static AppConfig()
        {
            AppSettings = new AppSettings();
            Connections = new Connections();
        }

        public static readonly AppSettings AppSettings;
        public static readonly Connections Connections;
    }
}