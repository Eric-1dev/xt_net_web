using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eric.DynamicArray
{
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
