using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task_4_1_1
{
    static class Indexer
    {
        public static string FileMD5(string path)
        {
            var content = new List<string>(MyFileMFuncs.ReadAllLines(path, FileMode.Open));
            return StringArrMD5(content);
        }
        public static string StringArrMD5(IEnumerable<string> arr)
        {

            var str = new StringBuilder();
            foreach (var line in arr)
                str = str.Append(line);
            byte[] stringData = new byte[str.Length * 2]; // *2 т.к. char занимает 2 байта
            for (int i = 0; i < str.Length; i += 2)
            {
                stringData[i] = (byte)(str[i] >> 8);
                stringData[i+1] = (byte)(str[i] & 0xFF);
            }
            return CalculateMD5(stringData);
        }
        private static string CalculateMD5(byte[] byteData)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(byteData);
            return BitConverter.ToString(checkSum);
        }
    }
}
