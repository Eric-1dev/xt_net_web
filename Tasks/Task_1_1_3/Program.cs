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
            const byte MAX_N = 50;

            do
            {
                Console.Write("Введите N ");
                str = Console.ReadLine();
                byte.TryParse(str, out n);
                if (n > MAX_N)
                    Console.WriteLine("Многовато будет");
                if (n < 1)
                    Console.WriteLine("Маловато будет");
            }
            while (n > MAX_N || n < 1);

            for ( byte i = 0; i < n; i++ )
            {
                for (byte j = 0; j < n - i; j++)
                {
                    Console.Write(" "); // Добавляем необходимое количество пробелов в начало строки
                }
                for (byte j = 0; j < 2 * i + 1; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
    }
}
