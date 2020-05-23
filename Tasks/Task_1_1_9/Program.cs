using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_1_9 // NON-NEGATIVE SUM
{
    class Program
    {
        static void Main(string[] args)
        {
            // Заполнение одномерного массива уже было в задании 1_1_7, поэтому просто копируем код :)
            string str;
            byte n; // Больше 255 элементов нам ни к чему - получим нечитаемый вывод
            UInt16 sum = 0;

            Console.Write("Введите число элементов: ");
            str = Console.ReadLine();
            byte.TryParse(str, out n);

            sbyte[] arr = new sbyte[n]; // Возьмем sbyte'овый массив, чтобы не смотреть на длинные числа в int'е

            Random rnd = new Random();

            for (byte i = 0; i < n; i++) // Заполняем массив случайными числами и сразу же считаем сумму неотрицательных
            {
                arr[i] = (sbyte)rnd.Next(-127, 128);
                if (arr[i] >= 0)
                    sum += (UInt16)arr[i];
            }

            printArr(arr); // Выводим получившийся массив

            Console.WriteLine("Сумма неотрицательных элементов равна: " + sum);
        }

        static void printArr<T>(T[] array)
        {
            foreach (var elem in array)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine();
        }
    }
}
