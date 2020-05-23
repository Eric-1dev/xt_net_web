using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1 = "написать программу, которая";
            string str2 = "описание";

            for (int i = 0; i < str1.Length; i++)
            {
                if (str2.Contains(str1[i]))
                {
                    str1 = str1.Insert(i+1, str1[i].ToString());
                    ++i;
                }
            }

            Console.WriteLine(str1);
        }
    }
}
