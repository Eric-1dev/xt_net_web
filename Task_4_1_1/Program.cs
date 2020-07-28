using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Task_4_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            INotify notify = new Notify();
            switch (Environment.GetCommandLineArgs().Length)
            {
                case 2:
                    var dir = Environment.GetCommandLineArgs()[1];
                    if (IsPathCorrect(dir))
                    {
                        var watcher = new Watcher(dir);
                        watcher.Run();
                    }
                    else
                        notify.Show("Directory name is incorrect");
                    break;
                case 3:
                    var file = Environment.GetCommandLineArgs()[1];
                    var dateString = Environment.GetCommandLineArgs()[2];
                    DateTime date;
                    if (IsPathCorrect(file) && DateTime.TryParse(dateString, out date))
                    {

                    }
                    break;
                default:
                    Console.WriteLine($"Usage: {Environment.NewLine}\t{Path.GetFileName(Environment.GetCommandLineArgs()[0])} <directory-name> - for watching" +
                                         $"{Environment.NewLine}\t{Path.GetFileName(Environment.GetCommandLineArgs()[0])} <fullpath-to-file> <recovery-date> - for recover file on selected date");
                    Console.ReadKey();
                    break;
            }
        }

        public static bool IsPathCorrect(string _path)
        {
            var path = new FileInfo(_path);
            if (path.Exists)
                return true;
            return false;
        }
    }
}
