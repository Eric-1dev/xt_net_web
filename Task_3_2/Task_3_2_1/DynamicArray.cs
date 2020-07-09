using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eric.DynamicArray
{
    public class DynamicArray<T> : IEnumerable<T>, IEnumerable, ICloneable
    {
        private T[] _arr;

        public DynamicArray()
        {
            _arr = new T[8];
            Length = 0;
        }
        public DynamicArray(int n)
        {
            _arr = new T[n];
            Length = 0;
        }
        public DynamicArray(IEnumerable<T> collection)
        {
            //_arr = collection.ToArray();
            int colCount = 0;
            int i = 0;
            foreach (var item in collection)
                colCount++;
            _arr = new T[colCount];
            foreach (var item in collection)
                _arr[i++] = item;
            Length = _arr.Length;
        }
        public int Length { get; private set; }
        public int Capacity 
        {
            get => _arr.Length; 
            /*
             * Если Capacity увеличивается, то копируем все элементы массива (Length).
             * Если уменьшается - то переносим только те, что умещаются в новый Capacity (value)
             */
            set
            {
                //int i;
                T[] _newArr = new T[value];

                /*for (i = 0; i < Math.Min(Length, value); i++)
                    _newArr[i] = _arr[i];*/
                Length = CopyTo(_newArr, 0, Math.Min(Length, value) - 1);
                _arr = _newArr;
            }
        }
        
        public void Add(T value)
        {
            if (Length == Capacity)
            {
                T[] newArr = new T[Capacity * 2];
                CopyTo(newArr, 0, Length - 1);
                _arr = newArr;
            }
            _arr[Length] = value;
            Length++;
        }

        public bool Insert(T value, int index)
        {
            if (index > Length) throw new ArgumentOutOfRangeException("index", "Index must be greater than 0");

            if (index == Length) { Add(value); return true; }    // Добавим возможность добавить элемент после последнего (т.е. в конец массива)

            if ( Length == Capacity ) // Если длина не позволяет - расширяем массив
            {
                T[] newArr = new T[Capacity * 2];
                CopyTo(newArr, 0, Length - 1);
                _arr = newArr;
            }

            for (int i = Length - 1; i >= index; i--)
            {
                _arr[i + 1] = _arr[i];
            }
            _arr[index] = value;
            Length++;
            return true;
        }
        public void AddRange(IEnumerable<T> collection)
        {
            //if (Length + (collection as ICollection).Count > Capacity)
            int colCount = 0;
            foreach (var item in collection) // Т.к. IEnumerable<T> не предполагает обязательного наличия метода или свойста Count, то посчитаем количество элементов вот так
                colCount++;
            if ( Length + colCount > Capacity )
            {
                T[] newArr = new T[Length + colCount];
                CopyTo(newArr, 0, Length - 1);
                _arr = newArr;
            }
            int i = Length;
            foreach (var item in collection)
                _arr[i++] = item;
            Length = i;
        }
        public bool Remove(T value)
        {
            for (int i = 0; i < Length; i++)
            {
                if (_arr[i].Equals(value))
                {
                    for (int j = i + 1; j < Length; j++)
                        _arr[i] = _arr[j];
                    _arr[Length - 1] = default; // Не обязательно, но так красивее
                    Length--;
                    return true;
                }
            }
            return false;
        }
        public int CopyTo(Array newArr, int FirstIndex, int LastIndex)
        {
            if (FirstIndex > LastIndex) throw new ArgumentException("FirstIndex, LastIndex", "FirstIndex must be less than LastIndex");
            if (LastIndex >= Length) throw new ArgumentOutOfRangeException("LastIndex", "LastIndex must be less then Length");
            int j = 0;
            for (int i = FirstIndex; i <= LastIndex; i++)
            {
                newArr.SetValue(_arr[i], j++);
            }
            return j;
        }

        public int CopyTo(Array newArr, int FirstIndex) => CopyTo(newArr, FirstIndex, Length - 1);

        public T this[int index]
        {
            get
            {
                if (index >= 0)
                {
                    if (index >= Length) throw new ArgumentOutOfRangeException("index", "Index must be less than Length");
                    return _arr[index];
                }
                else
                {
                    if ( -index > Length ) throw new ArgumentOutOfRangeException("index", "Negative index must be less or equal then Length");
                    return _arr[Length + index];
                }
            }

            set
            {
                if (index >= 0)
                {
                    if (index >= Length) throw new ArgumentOutOfRangeException("index", "Index must be less than Length");
                    _arr[index] = value;
                }
                else
                {
                    if (-index > Length) throw new ArgumentOutOfRangeException("index", "Negative index must be less or equal then Length");
                    _arr[Length + index] = value;
                }
            }
        }

        public T[] ToArray()
        {
            T[] _newArr = new T[Length];
            for (int i = 0; i < Length; i++)
            {
                _newArr[i] = _arr[i];
            }
            return _newArr;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => new Enumerator<T>(this);

        public object Clone()
        {
            int i = 0;
            var _temp = new DynamicArray<T>(Capacity);
            foreach (var elem in _arr)
            {
                _temp._arr[i++] = elem;
            }
            _temp.Length = Length;
            return _temp;
        }

        public class Enumerator<U> : IEnumerator<T>
        {
            protected DynamicArray<T> _collection;
            protected int _curIndex;

            public Enumerator(DynamicArray<T> collection)
            {
                Current = default;
                _curIndex = -1;
                _collection = collection;
            }

            public T Current { get; internal set; }

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                _curIndex++;
                if (_curIndex >= _collection.Length)
                    return false;
                else
                {
                    Current = _collection[_curIndex];
                    return true;
                }
            }

            public void Reset()
            {
                Current = default;
                _curIndex = -1;
            }
        }
    }

    public class CycledDynamicArray<T> : DynamicArray<T>, IEnumerable<T>
    {
        public CycledDynamicArray() : base() { }
        public CycledDynamicArray(int n) : base(n) { }
        public CycledDynamicArray(IEnumerable<T> collection) : base(collection) { }
        public new IEnumerator<T> GetEnumerator() => new CycledEnumerator<T>(this);
        IEnumerator IEnumerable.GetEnumerator() => new CycledEnumerator<T>(this);

        public class CycledEnumerator<U> : Enumerator<T>, IEnumerator<T>
        {
            public CycledEnumerator(CycledDynamicArray<T> collection) : base(collection) { }
            public new bool MoveNext()
            {
                _curIndex++;
                if (_curIndex >= _collection.Length)
                    _curIndex = 0;
                Current = _collection[_curIndex];
                return true;
            }
        }

        /*
         * Если поля в исходном Enumerator являются private
         * или по какой-то другой причине мы не можем унаследоваться от него,
         * то заново пишем реализацию IEnumerator с необходимым функционалом.
         */

        /*
        public class CycledEnumerator<U> : IEnumerator<T>
        {
            protected CycledDynamicArray<T> _collection;
            protected int _curIndex;

            public CycledEnumerator(CycledDynamicArray<T> collection)
            {
                Current = default;
                _curIndex = -1;
                _collection = collection;
            }

            public T Current { get; internal set; }

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                _curIndex++;
                if (_curIndex >= _collection.Length)
                    _curIndex = 0;
                Current = _collection[_curIndex];
                return true;
            }

            public void Reset()
            {
                Current = default;
                _curIndex = -1;
            }
        }
        */

    }
}
