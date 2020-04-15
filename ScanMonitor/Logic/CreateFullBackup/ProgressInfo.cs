namespace ScanMonitor.Logic.CreateFullBackup
{
    public class ProgressInfo
    {
        public int Total { get; internal set; }
        public int Current { get; internal set; }
        public string Text { get; internal set; }
    }
}