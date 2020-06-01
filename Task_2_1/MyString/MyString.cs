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
        private char[] _string;
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
            _length = num;
            _string = new char[(int)(num * _multiplier)];
        }

        // Конструктор, формирующий нашу строку из массива символов
        public MyString(char[] arr) : this(arr.Length)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                _string[i] = arr[i];
            }
        }

        // Конструируем нашу строку из штатного string
        public MyString(string str) : this(str.Length)
        {
            for (int i = 0; i < str.Length; i++)
            {
                _string[i] = str[(int)i];
            }
        }

        // Конструируем нашу строку из MyString
        public MyString(MyString str) : this(str.Length)
        {
            for (int i = 0; i < str.Length; i++)
            {
                _string[i] = str[i];
            }
        }

        // Конструируем нашу строку из MyString с указанной позиции, указанной длины
        public MyString(MyString str, int index, int length)
        {
            if (length > str.Length)
                length = str.Length;
            _length = length;
            _string = new char[(int)(length * _multiplier)];
            for (int i = index; i < i + length; i++)
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
         * Вспомогательный метод расширения массива.
         * Необязательный параметр указывает на необходимую длину массива
         */
        private void Expand(int targetLength)
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
                    Expand(index);
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

        // Перегрузка операторов сравнения == и !=
        public static bool operator ==(MyString str1, MyString str2) => Compare(str1, str2) == 0;
        public static bool operator !=(MyString str1, MyString str2) => Compare(str1, str2) != 0;
        //public override bool Equals(object obj) => Compare(base as MyString, obj as MyString) == 0;

        // Перезгрузка оператора +, как оператора конкатенации строк
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

        // Поиск последнего индекса символа. Возращает индекс поледнего символа в строке или -1, если его нет
        public int FindLast(char c)
        {
            int index = -1;
            for (int i = 0; i < _length; i++)
                if (_string[i] == c)
                    index = i;
            return index;
        }

        /*
         * Сравнение строк. Возвращает:
         *  0, если строки равны (посимвольно)
         *  1, если строки равны по длине
         *  2, если исходная строка длинее str
         *  3, если исходная строка короче str
         */

        static public int Compare(MyString a, MyString b)
        {
            // Нет смысла сравнивать посимвольно, если разная длина
            if (a?.Length > b?.Length)
                return 2;
            if (a?.Length < b?.Length)
                return 3;

            for (int i = 0; i < a?.Length; i++)
            {
                if (a?[i] != b?[i])
                    return 1;
            }
            return 0;
        }
        public int Compare(MyString str) => Compare(this, (MyString)str);

        /*
         * Аналог Replace у string, но т.к. у нас строка не константная - мы можем
         * заменять символы в самой строке, а не возвращать новую.
         * Возвращает количество замен
         */
        public int Replace(char _old, char _new)
        {
            int changes = 0;
            for (int i = 0; i < _length; i++)
            {
                if (_string[i] == _old)
                {
                    _string[i] = _new;
                    ++changes;
                }
            }
            return changes;
        }

        // Стирает указаное количество символов с указанной позиции
        public void Erase(int index, int length)
        {
            for (int i = index; i < index + length; i++)
            {
                _string[i] = (char)0;
            }
        }

        /*
         * Т.к. наша строка представляет из себя динамический массив,
         * и её длина может сокращаться, то неплохо бы было иметь возможность
         * уменьшать массив, если полезная длина существенно меньше capacity.
         * Fit обрезает массив, оставляя с первого ненулевого символа по последний
         */
        public void Fit()
        {
            int firstIndex = 0;
            int lastIndex = _string.Length - 1;

            for (int i = 0; i < _string.Length && _string[i] == 0; i++)
            {
                    firstIndex = i;
            }

            for (int i = _string.Length - 1; i > 0 && _string[i] == 0; i--)
            {
                    lastIndex = i;
            }

            char[] temp = new char[lastIndex - firstIndex];
            for (int i = 0; i < temp.Length - 1; i++)
            {
                temp[i] = _string[firstIndex + i + 1];
            }
            _string = null;
            _string = temp;
            _length = lastIndex - firstIndex;
        }

        // Естественно оверрайдим ToString
        override public string ToString() => new string(_string, 0, _length);
    }
}
