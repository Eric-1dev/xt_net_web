using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    
    static public class myArray
    {
        // Прошу прощения, что редактирую задание после дэдлайна, но после сегодняшнего урока
        // нашел способ применить более красивое, как мне кажется, решение.
        // Принципиально оно ничего не меняет, но сокращает код.
        // Кажется начинаю привыкать к отсутствию указателей :)
        // Очень хочется тут применить switch-case, вместо if'ов, но не придумал как.
        static public void printArr<T>(object array, uint x = 0, uint y = 0, uint z = 0)
        {
            if ( array is T[] ) // Одномерный массив
            {
                foreach (var elem in array as T[])
                {
                    Console.Write(elem + " ");
                }
                Console.WriteLine();
            }
            else if ( array is T[,] ) // двухмерный массив
            {
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        Console.Write((array as T[,])[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
            else if ( array is T[,,] ) // трехмерный массив
            {
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        for (int k = 0; k < z; k++)
                        {
                            Console.Write((array as T[,,])[i, j, k] + "\t");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
