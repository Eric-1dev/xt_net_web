using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Eric.String
{
    public class MyString
    {
        char[] _string;
        private int _length = 0; // Полезная длина массива. Т.е. длина пользовательских данных в массиве
        private float _multiplier = (float)1.5; // Множитель. Определяет коэффициент запаса пустых ячеек массива при расширении

        // Конструктор без параметров. Просто создаёт объект класса
        public MyString() { }

        /*
         * Конструктор, с указанием длины строки.
         * Создает массив чаров длиной num * multiplier
         */
        public MyString(int num)
        {
            _init(num);
        }

        // Конструктор, формирующий нашу строку из массива символов
        public MyString(char[] arr)
        {
            // this->MyString(arr.Length); // Так почему-то нельзя, поэтому придется сделать вспомогательный метод _init
            _init(arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                _string[i] = arr[i];
            }
        }

        // Конструируем нашу строку из штатного string
        public MyString(string str)
        {
            _init(str.Length);
            for (int i = 0; i < str.Length; i++)
            {
                _string[i] = str[(int)i];
            }
        }

        // Конструируем нашу строку из MyString
        public MyString(MyString str)
        {
            _init(str.Length);
            for (int i = 0; i < str.Length; i++)
            {
                _string[i] = str[i];
            }
        }

        // Деструктор. Хотя в шарпе, наверное, и не нужен
        ~MyString()
        {
            _string = null;
        }

        /*
         * Вспомогательный метод, используемый в конструкторах
         */
        private void _init(int num)
        {
            _length = num;
            _string = new char[(int)(num * _multiplier)];
        }

        /*
         * Вспомогательный метод расширения массива.
         * Необязательный параметр указывает на необходимую длину массива
         */
        private void _expand(int targetLength)
        {
            char[] _temp = new char[(int)(targetLength * _multiplier)];
            for (int i = 0; i < _length; i++)
            {
                _temp[i] = _string[i];
            }
            _string = null; // Необязательная строка. Но покажем сборщику мусора, что старые данные нам больше не нужны
            _string = _temp;
        }

        /* 
         * Перегружаем оператор индексирования
         * Сначала сделал проверку на соответствие индекса длине массива,
         * но потом подумал, что в случае превышения длины - и так
         * должен выпасть out of range.
         */
        public char this[int index]
        {
            get => _string[index];
            set
            {
                if (index >= _string.Length) // Если индекс выходит за размеры массива, то расширяем массив
                {
                    _expand(index);
                    _length = index + 1;
                }
                if (index >= _length) // Если индекс больше полезной длины, то задаем новую полезную длину
                    _length = index + 1;
                _string[index] = value;
            }
        }

        // Геттер для ёмкости массива
        public uint Capacity
        {
            get => (uint)_string.Length;
        }

        // Геттер для полезного размера массива
        public int Length
        {
            get => _length;
        }

        // Геттер и сеттер для множителя
        public float Multilier
        {
            get => _multiplier;
            set
            {
                if (value > 1 && value <= 3) // Множитель должен быть больше 1 и меньше 3 (слишком много памяти для резерва выделять не стоит)
                    _multiplier = value;
                else
                    _multiplier = (float)1.5;
            }
        }

        // Оператор приведения string к MyString
        public static implicit operator MyString(string str) => new MyString(str);

        // Оператор приведения MyString к string
        public static implicit operator string(MyString str) => str.ToString();

        // Пперезгрузка оператора +, как оператора конкатенации строк
        public static MyString operator +(MyString str1, MyString str2) => str1.Concat(str2);

        // Конкатенация строк
        public MyString Concat(string str)
        {
            MyString temp = new MyString(_string);
            for (int i = 0; i < str.Length; i++)
            {
                temp[_length + i] = str[i];
            }
            return temp;
        }

        public MyString Concat(MyString str) => Concat((string)str);

        public char[] ToCharArray()
        {
            char[] temp = new char[_length];
            for (int i = 0; i < _length; i++)
            {
                temp[i] = _string[i];
            }
            return temp;
        }

        // Поиск символа. Возращает индекс символа в строке или -1, если его нет
        public int Find(char c)
        {
            for (int i = 0; i < _length; i++)
                if (_string[i] == c)
                    return i;

            return -1;
        }

        /*
         * Сравнение строк. Возвращает:
         *  0, если строки равны (посимвольно)
         *  1, если строки равны по длине
         *  2, если исходная строка длинее str
         *  3, если исходная строка короче str
         */
        public int Compare(MyString str)
        {
            // Нет смысла сравнивать посимвольно, если разная длина
            if (this.Length > str.Length)
                return 2;
            if (this.Length < str.Length)
                return 3;

            bool flag = true;
            for (int i = 0; i < _length; i++)
            {
                if (this[i] != str[i])
                    flag = false; // если нашли отличия - сбрасываем флаг
            }
            if (flag)
                return 0;
            return 1;
        }

        // Естественно оверрайдим ToString
        override public string ToString() => new string(_string, 0, _length);
    }
}
