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
            if (Environment.GetCommandLineArgs().Length != 2 && Environment.GetCommandLineArgs().Length != 3)
            {
                Console.WriteLine($"Usage: {Environment.NewLine}\t{Path.GetFileName(Environment.GetCommandLineArgs()[0])} <directory-name> - for watching" +
                                         $"{Environment.NewLine}\t{Path.GetFileName(Environment.GetCommandLineArgs()[0])} <fullpath-to-file> <recovery-date> - for recover file on selected date");
                Console.ReadKey();
                return;
            }
            if (Environment.GetCommandLineArgs().Length == 2)
            {
                Watcher watcher = new Watcher();
                watcher.Run(Environment.GetCommandLineArgs()[1]);
            }
        }
    }
}
