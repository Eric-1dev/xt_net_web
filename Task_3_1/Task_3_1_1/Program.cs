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

public class Human
{
    private static int _numbers = 1;
    public int Number { get; private set; }
    public string Name { get; private set; }
    public Human(string name)
    {
        Name = name;
        Number = _numbers++;
    }
    public override bool Equals(object obj)
    {
        return Name == (obj as Human).Name && Number == (obj as Human).Number;
    }
}


/* 
 * Задачу можно было решить с помощью встроенного LinkedList,
 * но с целью лучшего понимания работы коллекций
 * напишем собственную коллекцию, представляющую из себя
 * кольцевой односвязный список.
 */
public class CircularLinkedList<T> : ICollection<T>, IEnumerable<T>
{
    public CircularLinkedList()
    {
        Count = 0;
    }

    public int Count { get; private set; }
    public CircularLinkedListNode<T> First { get; private set; }
    public bool IsReadOnly => false;

    public void Add(T item)
    {
        CircularLinkedListNode<T> newNode = new CircularLinkedListNode<T>(item);
        newNode.List = this;
        if (Count == 0)
        {
            First = newNode;
            newNode.Next = newNode;
        }
        else
        {
            CircularLinkedListNode<T> lastNode = First.Next;

            // Пробегаемся по списку в поисках последнего элемента
            while(lastNode.Next != First)
            {
                lastNode = lastNode.Next;
            }
            lastNode.Next = newNode;
            newNode.Next = First;
        }
        Count++;
    }

    public void Clear()
    {
        First = null; // Ломаем ссылку первый элемент. Остальное вычистит Garbage Collecotor (вроде бы)
        Count = 0;
    }

    public bool Contains(T item)
    {
        foreach (var elem in this)
        {
            if (elem.Equals(item))
                return true;
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        CircularLinkedListNode<T> curNode = First;
        CircularLinkedListNode<T> prevNode;
        do
        {
            prevNode = curNode;
            curNode = curNode.Next;
            if (curNode.Value.Equals(item))
            {
                prevNode.Next = curNode.Next;
                Count--;
                return true;
            }
        }
        while (curNode != First);
        return false;
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public struct Enumerator : IEnumerator<T>
    {
        private CircularLinkedList<T> _list;
        private CircularLinkedListNode<T> _curNode;

        public Enumerator(CircularLinkedList<T> list)
        {
            _list = list;
            _curNode = null;
        }
        public T Current { 
            get
            {
                return _curNode.Value;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _curNode = null;
        }

        public bool MoveNext()
        {
            // Если _curNode пока не определен (первый вызов MoveNext), ставим его в начало списка и возращаемся
            if (_curNode == null)
            {
                _curNode = _list.First;
                return true;
            }

            // Если дошли по кругу до первого элемента - значит полностью обошли список
            if (_curNode.Next == _list.First)
                return false;
            else
                _curNode = _curNode.Next;
            return true;
        }

        public void Reset()
        {
            _curNode = null;
        }
    }
}

// Элемент нашей коллекции
public class CircularLinkedListNode<T>
{
    public CircularLinkedListNode(T value)
    {
        Value = value;
        Next = null;
    }

    public T Value { get; set; }

    public CircularLinkedListNode<T> Next { get; internal set; }
    public CircularLinkedList<T> List { get; internal set; }
}