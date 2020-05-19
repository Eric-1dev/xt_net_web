using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_1_3 // Another triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;
            byte n;
            Console.Write("Введите N ");
            str = Console.ReadLine();
            byte.TryParse(str, out n);
            if ( n > 100 )
            {
                Console.WriteLine("Многовато будет");
            }

            for ( byte i = 0; i < n; i++ )
            {
                for ( byte j = 0; j < n - i; j++ )
                {
                    Console.Write(" ");
                }
                for (byte j = 0; j < 2*i + 1; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            return;
        }
    }
}
