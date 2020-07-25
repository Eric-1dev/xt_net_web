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
            Watcher watcher = new Watcher();
            watcher.Run();
        }
    }
}
