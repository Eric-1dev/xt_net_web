using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_1_6 // FONT ADJUSTMENT
{
    class Program
    {
        static void Main(string[] args)
        {
            char c_key;
            byte key = 9;
            FontStyles textParam = 0;

            while ( key != 0 )
            {
                Console.Write("Параметры надписи: " + textParam);
                Console.WriteLine();

                Console.WriteLine("Введите:\n\t0: Exit\n\t1: Bold\n\t2: Italic\n\t3: Underline");
                c_key = Console.ReadKey().KeyChar;
                byte.TryParse(c_key.ToString(), out key);
                Console.WriteLine();

                switch (key) {
                    case 0:
                        Console.WriteLine("--- Выходим ---");
                        break;
                    case 1:
                        textParam ^= FontStyles.Bold;
                        break;
                    case 2:
                        textParam ^= FontStyles.Italic;
                        break;
                    case 3:
                        textParam ^= FontStyles.Underline;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод");
                        break;
                }
            }
        }

        [Flags]
        enum FontStyles : byte
        {
            None = 0,
            Bold = 1,
            Italic = 2,
            Underline = 4
        };
    }
}
