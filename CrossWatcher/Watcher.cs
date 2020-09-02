using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CrossWatcher
{
    public partial class Watcher : ServiceBase
    {
        private FileSystemWatcher watcher;
        public Watcher()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = false;

            watcher = new FileSystemWatcher();
            watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Created += OnChange;
            watcher.Changed += OnChange;
            watcher.Renamed += OnChange;
            watcher.InternalBufferSize = 65536;
        }

        protected override void OnStart(string[] args)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var configPath = path.Replace(".exe", ".cfg");
            string folder;
            using (var sr = new StreamReader(configPath))
            {
                folder = sr.ReadLine();
            }
            watcher.Path = folder;
            watcher.EnableRaisingEvents = true;
        }

        protected override void OnStop()
        {
            watcher.EnableRaisingEvents = false;
        }

        private static void OnChange(object source, FileSystemEventArgs e)
        {
            var name = new StringBuilder(e.Name);

            if (Path.GetExtension(e.FullPath).Equals(".arj", StringComparison.OrdinalIgnoreCase))
            {
                if (name[0] == 'h')
                {
                    name[0] = 'a';
                    RenameFile(e.FullPath, name.ToString());
                    return;
                }
                if (name[0] == 'g')
                {
                    name[0] = 'l';
                    RenameFile(e.FullPath, name.ToString());
                    return;
                }
                if (name[0] == 'i' ||
                         name[0] == 'j' ||
                         name[0] == 'p' ||
                         name[0] == 'q')
                {
                    DeleteFile(e.FullPath);
                    return;
                }
                return;
            }
            if (Path.GetExtension(e.FullPath).Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(e.FullPath);
                    var content = sr.ReadLine();
                    sr.Close();
                    if (content.Split(';').Count() != 76)
                    {
                        DeleteFile(e.FullPath);
                        return;
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (sr != null)
                        sr.Close();
                }
                
                if (name.ToString().Equals("Base.txt", StringComparison.OrdinalIgnoreCase))
                {
                    int i = 0;
                    do
                    {
                        i++;
                        name = new StringBuilder($"Base_{i}.txt");
                    }
                    while (File.Exists(e.FullPath.Replace(e.Name, name.ToString()).ToString()));

                    RenameFile(e.FullPath, name.ToString());
                    return;
                }
                return;
            }
            DeleteFile(e.FullPath);
            return;
        }

        private static void RenameFile(string oldFullPath, string newName)
        {
            Thread thread = new Thread(() =>
            {
                var newFullPath = oldFullPath.Replace(Path.GetFileName(oldFullPath), newName);

                bool ready = false;
                for (int i = 0; i < 10 && !ready; i++)
                {
                    try
                    {
                        if (File.Exists(newFullPath) && File.Exists(oldFullPath))
                            File.Delete(newFullPath);

                        File.Move(oldFullPath, newFullPath);
                        ready = true;
                    }
                    catch (Exception)
                    {
                        // TODO nothing to do - it is a service, so log only
                    }
                    Thread.Sleep(300);
                }
            });
            thread.Start();
        }

        private static void DeleteFile(string fullPath)
        {
            Thread thread = new Thread(() =>
            {
                bool ready = false;
                for (int i = 0; i < 10 && !ready; i++)
                {
                    try
                    {
                        File.Delete(fullPath);
                        ready = true;
                    }
                    catch (Exception)
                    {
                        // TODO nothing to do - it's a service, so log only
                    }
                    Thread.Sleep(300);
                }

            });
            thread.Start();
        }
    }
}
