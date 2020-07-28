using System;

namespace Task_4_1_1
{
    // Уведомление через командную строку
    class Notify : INotify
    {
        public void Show(string str)
        {
            Console.WriteLine($"{str} + time: { DateTime.Now}");
        }
    }
}
