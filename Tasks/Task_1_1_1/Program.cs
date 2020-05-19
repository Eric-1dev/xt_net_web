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
            string str; // Временная переменная, для хранения считанной строки
            Int16 a, b; // Габариты прямоугольника
            Console.Write("Расчет площади прямоугольника:\na = ");
            str = Console.ReadLine();
            Int16.TryParse(str, out a); // Парсим строку в int16, игнорируя возращаемое значение метода, т.к. по условию задачи вводятся только числа
            Console.Write("b = ");
            str = Console.ReadLine();
            Int16.TryParse(str, out b);
            if ( a <= 0 || b <= 0 ) // Вылетаем, если значения не удовлетворяют условию
            {
                Console.WriteLine("Введены некорректные значения длины и ширины");
                return;
            }
            Console.WriteLine("Площадь прямоугольника равна " + a * b);
            return;
        }
    }
}
