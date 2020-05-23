using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_1_8 // NO POSITIVE
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;
            uint n;

            Console.Write("Введите размер массива: ");
            str = Console.ReadLine();
            uint.TryParse(str, out n);

            int[,,] arr = new int[n, n, n]; // Возьмем массив в виде куба со стороной n

            Random rnd = new Random();

            // Заполняем массив случайными числами
            for (uint i = 0; i < n; i++)
                for (uint j = 0; j < n; j++)
                    for (uint k = 0; k < n; k++)
                        arr[i, j , k] = rnd.Next(-1000, 1000);

            Console.WriteLine("-------- Исходный массив: --------");
            printArr(arr, ref n, ref n, ref n);

            // Пробегаемся по массиву и заменяем положительные элементы нулями
            for (uint i = 0; i < n; i++)
                for (uint j = 0; j < n; j++)
                    for (uint k = 0; k < n; k++)
                        if (arr[i, j, k] > 0)
                            arr[i, j, k] = 0;

            Console.WriteLine("-------- Новый массив: --------");
            printArr(arr, ref n, ref n, ref n);
        }

        // Вывод трехмерного массива размерностью x y z, в виде z таблиц размером x * y
        // Метод универсальный, для массивов разных типов и размерностей. Все переменные передаем
        // ссылками, чтобы не плодить одинаковые данные в памяти.
        static void printArr<T>(T[,,] array, ref uint x, ref uint y, ref uint z)
        {
            for (int i = 0; i < z; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < x; k++)
                    {
                        Console.Write(array[i, j, k] + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
