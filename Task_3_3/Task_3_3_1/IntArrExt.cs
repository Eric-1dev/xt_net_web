using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_3_1
{
    public static class IntArrExt
    {
        public static void Action(this int[] arr, Func<int, int> act)
        {
            if (arr == null) throw new NullReferenceException();
            for (int i = 0; i < arr.Length; i++)
                arr[i] = act(arr[i]);
        }

        public static void PrintArr(this int[] arr)
        {
            if (arr == null) throw new NullReferenceException();
            foreach (var item in arr)
                Console.Write($"{item}\t");
            Console.WriteLine();
        }

        public static void PrintArr(this int[] arr, string comment)
        {
            Console.WriteLine(comment);
            arr.PrintArr();
        }

        public static void FillRandom(this int[] arr, int min, int max)
        {
            if (arr == null) throw new NullReferenceException();
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

        // Grouping by value, ordering groups by descending and select first of them
        public static int Prevalent(this int[] arr) => arr.GroupBy(a => a).OrderByDescending(b => b.Count()).FirstOrDefault().Key;
    }
}
