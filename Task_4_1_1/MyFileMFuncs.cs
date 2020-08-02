using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_4_1_1
{
    static class MyFileMFuncs
    {
        // Свой ReadAllLines, который, в отличие от File.ReadAllLines считывает файл, даже если он открыт в блокноте
        static internal IEnumerable<string> ReadAllLines(string path, FileMode mode)
        {
            Thread.Sleep(10); // Немного поспим, т.к. изменения могуть быть частыми и файл не успеет разблокироваться
            using (var fileStream = new FileStream(path, mode, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(fileStream))
            {
                LinkedList<string> file = new LinkedList<string>();

                while (!streamReader.EndOfStream)
                    file.AddLast(streamReader.ReadLine());
                return file.ToList<string>();
            }
        }
    }
}
