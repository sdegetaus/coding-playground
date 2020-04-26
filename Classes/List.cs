using DataStructures.Utilities;

namespace DataStructures
{
    public class List<T> : Collection<T>
    {
        public T this[int index]
        {
            get
            {
                if (index == -1)
                {
                    return items[Count - 1];
                }

                if (index < 0 || index >= Count)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                return items[index];
            }

            set
            {
                if (index == -1)
                {
                    items[Count - 1] = value;
                    return;
                }

                if (index < 0 || index >= Count)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                items[index] = value;
            }
        }

        public List() : this(0) { }

        public List(int capacity)
        {
            if (capacity < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            items = new T[capacity];
        }

        public void Add(params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                AddToEnd(items[i]);
            }
        }

        public new void Remove(T item)
        {
            base.Remove(item);
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(items[i]))
                {
                    return true;
                }
            }
            return false;
        }

    }
}