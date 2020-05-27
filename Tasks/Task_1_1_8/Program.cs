using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

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
            myArray.printArr<int>(arr, n, n, n);

            // Пробегаемся по массиву и заменяем положительные элементы нулями
            for (uint i = 0; i < n; i++)
                for (uint j = 0; j < n; j++)
                    for (uint k = 0; k < n; k++)
                        if (arr[i, j, k] > 0)
                            arr[i, j, k] = 0;

            Console.WriteLine("-------- Новый массив: --------");
            myArray.printArr<int>(arr, n, n, n);
        }        
    }
}
