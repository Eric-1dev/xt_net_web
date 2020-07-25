using System;
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
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run()
        {
            string[] args = Environment.GetCommandLineArgs();

            // If a directory is not specified, exit program.
            /*if (args.Length != 2)
            {
                // Display the proper way to call the program.
                Console.WriteLine("Usage: Watcher.exe (directory)");
                return;
            }*/

            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher fsWatcher = new FileSystemWatcher(@"D:\123", "*.txt"))
            {
                //watcher.Path = args[1];
                //watcher.Path = @"D:\123";

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                fsWatcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Only watch text files.
                //watcher.Filter = "*.txt";

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
                Console.WriteLine("Press 'q' to quit.");
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Notify($"File: {e.FullPath} {e.ChangeType}");
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    break;
                case WatcherChangeTypes.Deleted:
                    DeletingFile(e);
                    break;
                case WatcherChangeTypes.Changed:
                    ChangingFile(e);
                    break;
                default:
                    break;
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            string oldDiffName = GetDiffFilename(e.OldFullPath);
            string newDiffName = GetDiffFilename(e.FullPath);
            try
            {
                File.Move(oldDiffName, newDiffName); // Переименовываем файл изменений, если переименован исходный
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

        private void ChangingFile(FileSystemEventArgs e)
        {
            Thread.Sleep(10); // Немного поспим, т.к. изменения могуть быть частыми и файл не успеет разблокироваться
            string[] content = ReadAllLines(e.FullPath);
            string diffName = GetDiffFilename(e.FullPath);

            // Открываем файл изменений или создаем новый, если его нет
            FileStream diffFileStream = null;
            try
            {
                diffFileStream = new FileStream(diffName, FileMode.OpenOrCreate);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot access diff-file", ex);
            }
            finally
            {
                if (diffFileStream != null)
                    diffFileStream.Close();
            }

            /*foreach (var item in content)
            {
                Console.WriteLine(item);
            }*/
        }

        // Свой ReadAllLines, который, в отличие от File.ReadAllLines считывает файл, даже если он открыт в блокноте
        private string[] ReadAllLines(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(fileStream))
            {
                LinkedList<string> file = new LinkedList<string>();

                while (!streamReader.EndOfStream)
                    file.AddLast(streamReader.ReadLine());
                return file.ToArray();
                
            }
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

        // Проверяем файл на блокировку
        private bool IsLocked(string path)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fileStream.Close();
                }
            }
            catch
            {
                return true;
            }
            return false;
        }

        private void Notify(string str)
        {
            Console.WriteLine($"{str} + time: { DateTime.Now}");
        }
    }
}
