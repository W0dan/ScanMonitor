namespace ScanMonitor.UI.Admin
{
    public class AdminItem : IHasId<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public interface IHasId<T>
    {
        T Id { get; set; }
    }
}