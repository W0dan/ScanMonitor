namespace ScanMonitor.UI.Extensions
{
    public static class StringExtensions
    {
        public static string Namify(this string value)
        {
            var result = value.Replace(" ", "");
            
            return result;
        }
    }
}