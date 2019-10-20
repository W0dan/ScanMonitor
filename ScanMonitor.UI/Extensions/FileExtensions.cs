using System.IO;

namespace ScanMonitor.UI.Extensions
{
    public static class FileExtensions
    {
        public static bool CanAccessFile(this string filename)
        {
            for (var i = 0; i < 10; i++)
            {
                try
                {
                    using (var fs = File.OpenRead(filename)) { }

                    return true; ;
                }
                catch (System.Exception)
                {
                    System.Threading.Thread.Sleep(100);
                }
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