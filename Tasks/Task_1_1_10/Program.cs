using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Task_1_1_10
{
    class Program
    {
        static void Main(string[] args)
        {
            byte x, y;
            string str;
            int sum = 0;

            Console.WriteLine("Введите размеры массива через пробел: ");
            str = Console.ReadLine();
            byte.TryParse(str.Split(' ')[0], out x);
            byte.TryParse(str.Split(' ')[1], out y);

            sbyte[,] arr = new sbyte[x, y];

            // Генерим 2D-массив с рандомными значениями
            Random rnd = new Random();

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    arr[i, j] = (sbyte)rnd.Next(-127, 128);
            // -----------------------------------------

            // Выводим массив
            myArray.printArr<sbyte>(arr, x, y);

            // Считаем сумму элементов массива, стоящих на чётных позициях
            for(int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    if ((i + j) % 2 == 0)
                        sum += arr[i, j];

            Console.WriteLine("Сумма элементов массива, стоящих на чётных позициях равна: " + sum);
        }     
    }
}
