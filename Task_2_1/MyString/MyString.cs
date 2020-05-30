using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eric.String
{
    public class MyString
    {
        char[] _string;
        private uint _capacity = 0;

        // Конструктор без параметров
        public MyString() { }

        /*
         * Конструктор, с указанием длины строки.
         * Создает массив чаров длиной num * 2 (привет StringBulder)
         */
        public MyString(int num)
        {
            _init((uint) num);
        }

        // Конструктор, формирующий нашу строку из массива символов
        public MyString(char[] arr)
        {
            // this->MyString(arr.Length); // Так почему-то нельзя, поэтому придется сделать вспомогательный метод _init
            _init((uint)arr.Length);
            for (uint i = 0; i < arr.Length; i++)
            {
                _string[i] = arr[i];
            }
        }

        // Конструируем нашу строку из штатного string
        public MyString(string str)
        {
            _init((uint)str.Length);
            for (uint i = 0; i < str.Length; i++)
            {
                _string[i] = str[(int)i];
            }
        }

        // Деструктор. Хотя в шарпе, наверное, и не нужен
        ~MyString()
        {
            this._string = null;
        }

        /*
         * Вспомогательный метод, используемый в конструкторах
         */
        private void _init(uint num)
        {
            this._capacity = (uint)num * 2;
            _string = new char[_capacity];
        }

        /* 
         * Перегружаем оператор индексирования
         * Сначала сделал проверку на соответствие индекса длине массива,
         * но потом подумал, что в случае превышения длины - и так
         * должен выпасть out of range.
         */
        public char this[uint index]
        {
            get => this._string[index];
            set => this._string[index] = value;
        }

        // Естественно оверрайдим ToString
        override public string ToString() => this._string.ToString();
    }
}
