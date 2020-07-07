﻿using System;
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
            arr = null;
            arr.PrintArr();

            Console.WriteLine(arr.SumOfAll());
            Console.WriteLine(arr.Avg());

            // Too simple for comment
            arr.Action(a => a * a);

            arr.PrintArr();
        }
    }

    public static class IntArrExt
    {
        public static void Action(this int[] arr, Func<int, int> act)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = act(arr[i]);
        }

        public static void PrintArr(this int[] arr)
        {
            foreach (var item in arr)
                Console.Write($"{item}\t");
            Console.WriteLine();
        }

        public static void FillRandom(this int[] arr, int min, int max)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(min, max);
        }

        public static int SumOfAll(this int[] arr)
        {
            if (arr == null) throw new NullReferenceException();
            return arr.Sum();
        }

        public static double Avg(this int[] arr)
        {
            if (arr == null) throw new NullReferenceException();
            return arr.Average();
        }

        //public static int Prevalent(this int[] arr)
        //{
            //var groups = arr.GroupBy<int[], >(arr.Count());
            //var tmp = arr.Where(a => arr.Count(a => a));
        //}
    }
}
