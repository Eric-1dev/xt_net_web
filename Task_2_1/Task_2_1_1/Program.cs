using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eric.String;

namespace Task_2_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyString str = "Hello World!!!";
            MyString str2 = "Hello World!!!";

            Console.WriteLine(str.Compare(str2));
        }
    }
}
