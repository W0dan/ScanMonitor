using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace ScanMonitor
{
    public class ScannedFilesWatcher
    {
        private readonly string _path;
        private readonly HashSet<string> _duplicateChecklist = new HashSet<string>();
        private readonly BlockingCollection<string> _scannedFiles = new BlockingCollection<string>();
        private FileSystemWatcher _fsw;

        public static ScannedFilesWatcher StartWatching(string path)
        {
            return new ScannedFilesWatcher(path);
        }

        private ScannedFilesWatcher(string path)
        {
            _path = path;

            ThreadPool.QueueUserWorkItem(Start);
        }

        private void Start(object state)
        {
            _fsw = new FileSystemWatcher(_path)
            {
                EnableRaisingEvents = true
            };

            _fsw.Created += FileCreated;
        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            var fi = new FileInfo(e.FullPath);

            var uniqueName = e.FullPath + "_" + fi.CreationTimeUtc.ToString("yyyyMMdd_HHmmss,fff");

            if (_duplicateChecklist.Contains(uniqueName))
                return;

            _duplicateChecklist.Add(uniqueName);
            _scannedFiles.Add(e.FullPath);
        }

        public IEnumerable<string> GetNextFile()
        {
            foreach (var scannedFile in _scannedFiles.GetConsumingEnumerable())
            {
                yield return scannedFile;
            }
        }
    }
}
