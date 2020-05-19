using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_1_2 // Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;
            Int16 n;
            Console.Write("Введите N = ");
            str = Console.ReadLine();
            Int16.TryParse(str, out n);
            if ( n > 100 )
            {
                Console.WriteLine("Многовато будет");
                return;
            }
            for (byte i = 0; i < n; i++)
            {
                for (byte j = 0; j < i + 1; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            return;
        }
    }
}
