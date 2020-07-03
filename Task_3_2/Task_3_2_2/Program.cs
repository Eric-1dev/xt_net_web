using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Eric.DynamicArray;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //List<int> arr = new List<int>();

            List<int> arr2 = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                arr2.Add(i + 20);
            }
            DynamicArray<int> arr = new DynamicArray<int>(arr2);


            Console.WriteLine("Adding elements in {for}");
            for (int i = 0; i < 11; i++)
                arr.Add(i);
            printArr(arr);

            Console.WriteLine("Arr capacity change:");
            arr.Capacity = 4;
            printArr(arr);

            Console.WriteLine("Adding range test");
            arr.AddRange(arr2);
            Console.WriteLine($"Length = {arr.Length} \tCapacity = {arr.Capacity}");
            printArr(arr);

            Console.WriteLine("ToArray test:\nArr3 now:");
            int[] arr3 = arr.ToArray();
            printArr(arr3);


            Console.WriteLine(arr[-3]);

            var arr4 = arr.Clone();
            printArr<int>(arr4 as DynamicArray<int>);
        }
        public static void printArr<T>(IEnumerable<T> coll)
        {
            foreach (var item in coll)
            {
                Console.WriteLine("Element = " + item);
            }
        }
    }
}