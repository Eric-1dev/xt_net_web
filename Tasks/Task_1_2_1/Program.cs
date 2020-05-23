using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            float avg;
            uint count = 0, sum = 0;

            string str = "Викентий хорошо отметил день рождения: покушал пиццу, посмотрел кино, пообщался со студентами в чате";
            string[] arr = str.Split(':', ',', ' ', '!', '?', ';', '.', '-');

            foreach (var elem in arr)
            {
                if ( elem.Length != 0)
                {
                    count++;
                    sum += (uint)elem.Length;
                    Console.WriteLine(elem);
                }
            }
            Console.WriteLine();

            avg = (float)sum / count; // В выводе округляем до целого по математическим плавилам
            Console.WriteLine("Средняя длина слова во введённой текстовой строке равна " + (uint)(avg + 0.5));
        }
    }
}
