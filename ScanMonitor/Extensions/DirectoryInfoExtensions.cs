using System.IO;

namespace ScanMonitor.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static void CopyTo(this DirectoryInfo sourceDi, DirectoryInfo destinationDi)
        {
            if (!destinationDi.Exists)
            {
                destinationDi.Create();
            }

            foreach (var sourceFile in sourceDi.GetFiles())
            {
                var destinationFileName = Path.Combine(destinationDi.FullName, sourceFile.Name);
                if (!File.Exists(destinationFileName))
                {
                    sourceFile.CopyTo(destinationFileName);
                }
            }

            foreach (var sourceDirectory in sourceDi.GetDirectories())
            {
                var destinationDirectoryName = Path.Combine(destinationDi.FullName, sourceDirectory.Name);
                sourceDirectory.CopyTo(new DirectoryInfo(destinationDirectoryName));
            }
        }
    }
}
