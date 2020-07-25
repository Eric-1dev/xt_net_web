using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3_1_1
{
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
}
