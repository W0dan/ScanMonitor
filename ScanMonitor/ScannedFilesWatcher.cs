using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace ScanMonitor
{
    public class ScannedFilesWatcher
    {
        private readonly string path;
        private readonly HashSet<string> duplicateChecklist = new HashSet<string>();
        private readonly BlockingCollection<string> scannedFiles = new BlockingCollection<string>();
        private FileSystemWatcher fileSystemWatcher;

        public static ScannedFilesWatcher StartWatching(string path)
        {
            return new ScannedFilesWatcher(path);
        }

        private ScannedFilesWatcher(string path)
        {
            this.path = path;

            ThreadPool.QueueUserWorkItem(Start);
        }

        private void Start(object state)
        {
            fileSystemWatcher = new FileSystemWatcher(path)
            {
                EnableRaisingEvents = true
            };

            fileSystemWatcher.Created += FileCreated;
        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            if (!CanProcessFile(e))
                return;

            var fi = new FileInfo(e.FullPath);

            var uniqueName = e.FullPath + "_" + fi.CreationTimeUtc.ToString("yyyyMMdd_HHmmss,fff");

            if (duplicateChecklist.Contains(uniqueName))
                return;

            duplicateChecklist.Add(uniqueName);
            scannedFiles.Add(e.FullPath);
        }

        private static bool CanProcessFile(FileSystemEventArgs e)
        {
            return !e.FullPath.Contains(".tmp");
        }

        public IEnumerable<string> GetNextFile()
        {
            foreach (var scannedFile in scannedFiles.GetConsumingEnumerable())
            {
                yield return scannedFile;
            }
        }
    }
}
