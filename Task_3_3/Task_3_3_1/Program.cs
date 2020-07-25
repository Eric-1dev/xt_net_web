using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10];

            // Filling array with random numbers like this
            arr.FillRandom(-10, 10);
            // or this
            var rnd = new Random();
            arr.Action(a => rnd.Next(-10, 10));

            // Printing array
            arr.PrintArr();

            Console.WriteLine("Sum of all elements: " + arr.SumOfAll());
            Console.WriteLine("Average value of all elements: " + arr.Avg());
            Console.WriteLine("Most prevalent element: " + arr.Prevalent());

            // Too simple for comment
            arr.Action(a => a * a);

            arr.PrintArr("New array:");
        }
    }
}
