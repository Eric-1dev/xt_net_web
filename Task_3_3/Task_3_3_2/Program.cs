using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "01FA#1";

            Console.WriteLine(str.ShowMyType());
        }
    }

    public static class SuperString
    {
        public static StringTypes ShowMyType(this string str)
        {
            string rus = "абвгдеёзжийклмнопрстуфхцчшщъыьэюя";
            string eng = "abcdefghijklmnopqrstuvwxyz";
            string dec = "0123456789";
            string hex = "0123456789abcdef";
            str = str.ToLower(); // More CPU calculations, but less RAM required
            if (str.Except(rus).Count() == 0)
                return StringTypes.Russian;
            if (str.Except(eng).Count() == 0)
                return StringTypes.English;
            if (str.Except(dec).Count() == 0)
                return StringTypes.Decimal;
            if (str.Except(hex).Count() == 0)
                return StringTypes.Heximal;
            return StringTypes.Mixed;
        }
    }

    public enum StringTypes
    {
        English,
        Russian,
        Decimal,
        Heximal,
        Mixed
    }
}
