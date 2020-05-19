using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string a_str, b_str;
            Int16 a, b;
            Console.Write("Расчет площади прямоугольника:\na = ");
            a_str = Console.ReadLine();
            Int16.TryParse(a_str, out a);
            Console.Write("b = ");
            b_str = Console.ReadLine();
            Int16.TryParse(b_str, out b);
            if ( a <= 0 || b <= 0 )
            {
                Console.WriteLine("Введены некорректные значения длины и ширины");
                return;
            }
            Console.WriteLine("Площадь прямоугольника равна " + a * b);
            return;
        }
    }
}
