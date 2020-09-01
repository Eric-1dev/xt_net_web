using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.ServiceProcess;
using System.Text;
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
            CanPauseAndContinue = true;

            watcher = new FileSystemWatcher();
            watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Created += OnChange;
            watcher.Changed += OnChange;
            watcher.Renamed += OnChange;
        }

        protected override void OnStart(string[] args)
        {
            //watcher.Path = args[1];
            watcher.Path = @"D:\123";
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
                else if (name[0] == 'g')
                {
                    name[0] = 'l';
                    RenameFile(e.FullPath, name.ToString());
                    return;
                }
                else if (name[0] == 'i' ||
                         name[0] == 'j' ||
                         name[0] == 'p' ||
                         name[0] == 'q')
                {
                    DeleteFile(e.FullPath);
                    return;
                }
            }
            else if (name.ToString().Equals("Base.txt", StringComparison.OrdinalIgnoreCase))
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
            else if (Path.GetExtension(e.FullPath).Equals(".dbf", StringComparison.OrdinalIgnoreCase) ||
                     Path.GetExtension(e.FullPath).Equals(".xls", StringComparison.OrdinalIgnoreCase))
            {
                DeleteFile(e.FullPath);
                return;
            }
        }

        private static void RenameFile(string oldFullPath, string newName)
        {
            var newFullPath = oldFullPath.Replace(Path.GetFileName(oldFullPath), newName);

            try
            {

                if (File.Exists(newFullPath))
                {
                    File.Delete(newFullPath);
                    Thread.Sleep(300);
                }
                File.Move(oldFullPath, newFullPath);
            }
            catch (Exception)
            {
                // TODO nothing to do - it is a service, so log only
            }
        }

        private static void DeleteFile(string fullPath)
        {
            try
            {
                File.Delete(fullPath);
            }
            catch (Exception)
            {
                // TODO nothing to do - it's a service, so log only
            }
        }
    }
}
