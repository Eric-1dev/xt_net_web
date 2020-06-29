using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ExtendedList<Human> extendedList = new ExtendedList<Human>();

            for (int i = 1; i <= 3; i++)
            {
                extendedList.AddLast(new Human("Вася-" + i));
            }

            for (int i = 0; i < 12; i++)
            {
                if (extendedList.Step())
                    extendedList.RemoveCurrent();
                Console.WriteLine(extendedList.CurrentPos.Value);
            }
        }
    }
}

// Расширим функционал связного списка, чтобы реализовать возможность кольцевого обхода
public class ExtendedList<T> : LinkedList<T>
{
    public LinkedListNode<T> CurrentPos { get; private set; }

    public bool Step()
    {
        if (Count != 0 && CurrentPos == null) // Заглушка, если текущая позиция не определена
            CurrentPos = First;
        else if (Count == 1)
            return false;
        else
            CurrentPos = ReferenceEquals(CurrentPos, Last) ? First : CurrentPos.Next;
        return true;
    }
    public bool RemoveCurrent()
    {
        LinkedListNode<T> _temp = CurrentPos;
        if (Step() != null )
        {
            Remove(_temp);
            return true;
        }
        return false;
    }
}

public class Human
{
    static private int _numbers = 1;
    public int Number { get; private set; }
    public string Name { get; private set; }
    public Human(string name)
    {
        Number = _numbers++;
        Name = name;
    }
    public override string ToString()
    {
        return Name + " " + Number;
    }
}