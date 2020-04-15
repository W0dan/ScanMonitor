using System;
using System.IO;

namespace ScanMonitor.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static void CopyTo(this DirectoryInfo sourceDi, DirectoryInfo destinationDi, Action<CopyToProgressInfo> reportProgressCallback)
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
                    reportProgressCallback(new CopyToProgressInfo { Text = $"Copy {sourceFile.FullName} to {destinationFileName}" });

                    sourceFile.CopyTo(destinationFileName);
                } else
                {
                    reportProgressCallback(new CopyToProgressInfo { Text = $"Skip {sourceFile.FullName}" });
                }
            }

            foreach (var sourceDirectory in sourceDi.GetDirectories())
            {
                var destinationDirectoryName = Path.Combine(destinationDi.FullName, sourceDirectory.Name);
                sourceDirectory.CopyTo(new DirectoryInfo(destinationDirectoryName), reportProgressCallback);
            }
        }

        public static int GetTotalNumberOfFiles(this DirectoryInfo sourceDi) 
        {
            var result = sourceDi.GetFiles().Length;

            foreach (var sourceDirectory in sourceDi.GetDirectories())
            {
                result += sourceDirectory.GetTotalNumberOfFiles();
            }

            return result;
        }
    }

    public class CopyToProgressInfo
    {
        public string Text { get; internal set; }
    }
}
