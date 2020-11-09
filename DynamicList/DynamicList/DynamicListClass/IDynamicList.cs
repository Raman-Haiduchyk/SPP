using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicList.DynamicListClass
{
    interface IDynamicList<T>
    {
        int Count { get; }
        T this[int index] { get; set; }
        void Add(T item);
        public bool Remove(T item);
        void RemoveAt(int index);
        void Clear();
    }
}
