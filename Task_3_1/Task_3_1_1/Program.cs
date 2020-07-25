using System;
using System.Collections;
using System.Collections.Generic;

namespace Task_3_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, divider;
            CircularLinkedList<Human> circularList = new CircularLinkedList<Human>();

            Console.WriteLine("Введите количество человек:");
            int.TryParse(Console.ReadLine(), out n);
            if ( n < 0 )
            {
                Console.WriteLine("N должно быть положительным.");
                return;
            }
            Console.WriteLine("Введите, какой по счету человек будет вычеркнут каждый раунд:");
            int.TryParse(Console.ReadLine(), out divider);
            if (divider > n)
            {
                Console.WriteLine("По условию задачи, данное число должо быть меньше общего числа людей.");
                return;
            }

            // Создаём "человеков"
            for (int i = 1; i <= n; i++)
            {
                circularList.Add(new Human("Вася"));
            }

            // Создаём ссылку на первый элемент кольцевого списка
            CircularLinkedListNode<Human> elem = circularList.First;

            int round = 1;
            int j = 1;
            // Бегаем по списку до тех пор, пока он не уменьшится до размеров делителя
            while (circularList.Count >= divider)
            {
                if ( j % divider == 0 )
                {
                    Console.WriteLine($"Раунд {round}. Вылетает участник {elem.Value.Name} с номером {elem.Value.Number}. \tИгроков осталось: {circularList.Count}");
                    round++;
                    circularList.Remove(elem.Value);
                }
                elem = elem.Next;
                j++;
            }
            Console.WriteLine("Игра окончена. Невозможно вычеркнуть больше людей.");
        }
    }
}