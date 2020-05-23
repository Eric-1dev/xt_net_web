using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    static public class myArray
    {
        // Вывод одномерного массива
        static public void printArr<T>(T[] array)
        {
            foreach (var elem in array)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine();
        }

        // Вывод двухмерного массива
        static public void printArr<T>(T[,] array, uint x, uint y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Вывод трехмерного массива размерностью x y z, в виде x таблиц размером z * y
        // Метод универсальный, для массивов разных типов и размерностей.
        static public void printArr<T>(T[,,] array, uint x, uint y, uint z)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < z; k++)
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
