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

            Console.WriteLine($"{str} {str.Length} {str.Capacity}");

            str.Erase(0, 3);
            str.Fit();

            Console.WriteLine($"{str} {str.Length} {str.Capacity}");

            str[12] = 'f';

            Console.WriteLine($"{str} {str.Length} {str.Capacity}");
        }
    }
}
