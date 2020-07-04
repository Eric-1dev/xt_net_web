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
            List<int> arr2 = new List<int>();

            for (int i = 0; i < 7; i++)
            {
                arr2.Add(i + 20);
            }

            //DynamicArray<int> dynArr = new DynamicArray<int>(arr2);
            DynamicArray<int> dynArr = new DynamicArray<int>();

            Console.WriteLine("Adding elements in {for}");
            for (int i = 0; i < 5; i++)
                dynArr.Add(i);

            //dynArr.Insert(22, 11);

            //var arr3 = new int[3];
            //dynArr.CopyTo(arr3, 9);

            printArr(dynArr);
            Console.WriteLine("-------------------");
            //printArr(arr3);

            var cycledArr = new CycledDynamicArray<int>(dynArr);
            Console.WriteLine("Cycled array:");
            printArr(cycledArr);

            /*Console.WriteLine("Array capacity change:");
            dynArr.Capacity = 4;
            printArr(dynArr);

            Console.WriteLine("Adding range test");
            dynArr.AddRange(arr2);
            Console.WriteLine($"Length = {dynArr.Length} \tCapacity = {dynArr.Capacity}");
            printArr(dynArr);

            Console.WriteLine("ToArray test:\nArr3 now:");
            int[] arr3 = dynArr.ToArray();
            printArr(arr3);


            Console.WriteLine(dynArr[-3]);

            var arr4 = dynArr.Clone();
            printArr<int>(arr4 as DynamicArray<int>);*/
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