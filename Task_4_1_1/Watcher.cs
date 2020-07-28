using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_4_1_1
{
    class Watcher
    {
        private string _directory;
        private static INotify notify = new Notify();
        public Watcher(string directory)
        {
            _directory = directory;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run()
        {
            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher fsWatcher = new FileSystemWatcher(_directory, "*.txt"))
            {
                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                fsWatcher.NotifyFilter = NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Include subdirectories
                fsWatcher.IncludeSubdirectories = true;

                // Add event handlers.
                fsWatcher.Changed += OnChanged;
                fsWatcher.Created += OnChanged;
                fsWatcher.Deleted += OnChanged;
                fsWatcher.Renamed += OnRenamed;

                // Begin watching.
                fsWatcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press <Escape> to exit.");
                while (Console.ReadKey().Key != ConsoleKey.Escape) ;
            }
        }

        public void RestoreFileToDate(string fullPath, DateTime date)
        {
            Diff diff = new Diff();
            diff.RestoreContentFormDiffFile(fullPath, date);
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            notify.Show($"File: {e.FullPath} {e.ChangeType}");
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Deleted:
                    DeletingFile(e);
                    break;
                case WatcherChangeTypes.Changed:
                    FindDiffs(e);
                    break;
                default:
                    break;
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Переименовываем файл изменений, если переименован исходный
            string oldDiffName = GetDiffFilename(e.OldFullPath);
            string newDiffName = GetDiffFilename(e.FullPath);
            try
            {
                File.Move(oldDiffName, newDiffName);
            }
            catch (FileNotFoundException)
            {
                // Если файла изменений нет, то и переименовывать не надо
            }
            catch (Exception ex)
            {
                throw new IOException("Cannot rename diff-file", ex);
            }
        }

        private void DeletingFile(FileSystemEventArgs e)
        {
            string diffName = GetDiffFilename(e.FullPath);
            try
            {
                File.Delete(diffName);
            }
            catch (Exception ex)
            {
                throw new IOException("Cannot delete diff-file", ex);
            }
        }

        private void FindDiffs(FileSystemEventArgs e)
        {
            string diffFullPath = GetDiffFilename(e.FullPath);
            Diff diff = new Diff();

            var result = diff.Compare(e.FullPath, diffFullPath);
            if (result != null)
                diff.WriteHistoryToFile(diffFullPath, result);
        }

        /// <summary>
        /// Getting name of diff-file from FullPath by replacing txt to tx_
        /// </summary>
        /// <param name="file"></param>
        /// <returns>name of diff-file from FullPath</returns>
        private string GetDiffFilename(string FullPath)
        {
            var str = new StringBuilder(FullPath);
            str[str.Length - 1] = '_';
            return str.ToString();
        }
    }
}
