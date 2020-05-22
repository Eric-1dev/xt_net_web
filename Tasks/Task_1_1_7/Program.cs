using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Task_1_1_7
{
    class Program
    {
        static void Main(string[] args)
        {
            sbyte min, max;
            string str;
            byte n; // Больше 255 элементов нам ни к чему - получим нечитаемый вывод

            Console.Write("Введите число элементов: ");
            str = Console.ReadLine();
            byte.TryParse(str, out n);

            sbyte[] arr = new sbyte[n]; // Возьмем sbyte'овый массив, чтобы не смотреть на длинные числа в int'е

            Random rnd = new Random();

            for (byte i = 0; i < n; i++) // Заполняем массив случайными числами
                arr[i] = (sbyte) rnd.Next(-127, 128);

            printArr(arr); // Выводим получившийся массив

            minMax<sbyte>(out min, out max, arr);  // Находим минимальное и максимальное значение

            Console.WriteLine("Минимальное значение в массиве: \t" + min);
            Console.WriteLine("Максимальное значение в массиве:\t" + max);

            sortArr(arr); // Сортируем

            printArr(arr); // Снова выводим
        }

        // Т.к. выводить массив придется 2 раза - логичней обернуть эту операцию в метод
        static void printArr(sbyte[] array)
        {
            foreach (var elem in array)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine();
        }

        // Скучно писать только под sbyte. Напишем универсальный метод под разные типы числовых значений
        static void minMax<T>(out T minVal, out T maxVal, T[] array) where T: IComparable<T>
        {
            maxVal = minVal = array[0];
            foreach (var elem in array)
            {
                if (maxVal.CompareTo(elem) < 0) maxVal = elem;
                if (minVal.CompareTo(elem) > 0) minVal = elem;
            }
        }

        // Аналогично поступим и с сортировкой
        static void sortArr<T>(T[] array) where T: IComparable<T>
        {
            T swap;
            // Сортируем пузирьком
            for ( uint i = 0; i < array.Length - 1; i++ )
                for ( uint j = 0; j < (uint)array.Length - i - 1; j++)
                    if ( array[j].CompareTo(array[j + 1]) > 0 )
                    {
                        swap = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = swap;
                    }
        }
    }
}
