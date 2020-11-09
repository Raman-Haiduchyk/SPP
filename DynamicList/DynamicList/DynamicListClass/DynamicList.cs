using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DynamicList.DynamicListClass
{
    class DynamicList<T>:IDynamicList<T>, IEnumerable
    {
        private T[] _array;

        public DynamicList() : this(0){ }

        public DynamicList(int index)
        {
            _array = new T[index];
        }

        public DynamicList(IEnumerable<T> collection)
        {
            _array = new T[0];
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        public T this[int index] 
        {
            get
            {
                if (!(index > -1 && index < _array.Length)) throw new ArgumentOutOfRangeException("index", "Index out of array range.");
                return _array[index];
            }   
            set
            {
                if (!(index > -1 && index < _array.Length)) throw new ArgumentOutOfRangeException("index", "Index out of array range.");
                _array[index] = value;
            }
        }

        public int Count => _array.Length;

        public void Add(T item)
        {
            Array.Resize(ref _array, _array.Length + 1);
            _array[_array.Length - 1] = item;
        }

        public void Clear()
        {
            Array.Resize(ref _array, 0);
        }

        public bool Remove(T item)
        {
            int index = Array.FindIndex(_array, (_item) => _item.Equals(item));
            if (index == -1) return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            int length = _array.Length;
            if (!(index > -1 && index < length)) throw new ArgumentOutOfRangeException("index", "Index out of array range.");
            if (index < length - 1) Array.Copy(_array, index + 1, _array, index, length - index - 1);
            Array.Resize(ref _array, length - 1);
        }

        public IEnumerator GetEnumerator()
        {
            return _array.GetEnumerator();
        }
    }
}
