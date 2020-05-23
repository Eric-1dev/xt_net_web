using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "я плохо учил русский язык. забываю начинать предложения с заглавной. хорошо, что можно написать программу!";

            bool nextToUpper = true; // Первое слово всегда должно начинаться с заглавной буквы.

            // Может было решение и проще, но в голову пришло это. К тому же, данный вариант отработает и разделители предложений в виде ?! и ...
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '.' || str[i] == '!' || str[i] == '?') // Если находим окончание предложения, то следующую попавшуюся букву поднимаем в верхний регистр
                {
                    nextToUpper = true;
                }

                if (char.IsLetter(str[i]) && nextToUpper)
                {
                    nextToUpper = false;
                    //char c = char.ToUpper(str[i]);
                    //str = str.Insert(i, c.ToString());
                    str = str.Insert(i, char.ToUpper(str[i]).ToString()); // Т.к. str[i] нельзя изменить - пришлось костылить через Insert/Remove
                    str = str.Remove(i + 1, 1);
                }
            }

            Console.WriteLine(str);
        }
    }
}
