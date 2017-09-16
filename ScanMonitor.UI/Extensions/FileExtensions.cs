using System.IO;

namespace ScanMonitor.UI.Extensions
{
    public static class FileExtensions
    {
        public static bool CanAccessFile(this string filename)
        {
            using (var fs = File.OpenRead(filename))
            {
            }

            return true;
        }

        public static string FileExtension(this string filename)
        {
            var fo = new FileInfo(filename);

            return fo.Extension.ToLowerInvariant();
        }
    }
}