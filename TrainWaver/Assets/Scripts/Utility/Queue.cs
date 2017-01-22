
using System.Collections.Generic;
namespace Utility
{
    public class Queue<T>
    {
        List<T> elements = new List<T>();

        public void Enqueue(T item)
        {
            elements.Add(item);
        }

        public T Dequeue()
        {
            T item = elements[0];
            elements.RemoveAt(0);
            return item;
        }

        public T GetLast()
        {
            if(Count > 0)
                return elements[elements.Count - 1];

            return default(T);
        }

        public T GetItem(int index)
        {
            return elements[index];
        }

        public int Count
        {
            get
            {
                return elements.Count;
            }
        }
    }
}
