namespace ScanMonitor.UI.Admin
{
    public class AdminItem : IHasId<string>, ICanBeCloned<AdminItem>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public AdminItem Clone()
        {
            return new AdminItem
            {
                Id = Id,
                Name = Name
            };
        }
    }

    public interface IHasId<T>
    {
        T Id { get; set; }
    }

    public interface ICanBeCloned<T>
    {
        T Clone();
    }
}