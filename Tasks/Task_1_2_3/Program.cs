using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            uint count = 0;
            string str = "Антон хорошо начал утро: послушал Стинга, выпил кофе и посмотрел Звёздные Войны";
            string[] arr = str.Split(':', ',', ' ', '!', '?', ';', '.', '-');

            foreach (var elem in arr)
            {
                if ( elem.Length > 0 )
                {
                    if (Char.IsLower(elem[0]))
                        count++;
                }
            }

            Console.WriteLine("Количество слов, начинающихся с маленькой буквы: " + count);
        }
    }
}
