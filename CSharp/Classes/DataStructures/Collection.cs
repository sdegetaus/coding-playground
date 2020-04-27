namespace CodingPlayground
{
    public abstract class Collection<T> : IDebug
    {
        protected T[] items;
        public int Capacity { get => items.Length; }
        public int Count { get; protected set; }
        public T FirstItem
        {
            get
            {
                if (Count == 0)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                return items[0];
            }

            protected set
            {
                if (Count == 0)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                items[0] = value;
            }
        }
        public T LastItem
        {
            get
            {
                if (Count == 0)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                return items[Count - 1];
            }

            protected set
            {
                if (Count == 0)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                items[Count - 1] = value;
            }
        }

        public Collection() : this(0) { }

        public Collection(int capacity)
        {
            if (capacity < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            items = new T[capacity];
        }

        public T[] ToArray()
        {
            var array = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                array[i] = items[i];
            }
            return array;
        }

        protected Collection<T> Clone()
        {
            var clone = this;
            clone.Count = Count;
            return clone;
        }

        protected void Swap(int index0, int index1)
        {
            T temp = items[index0];
            items[index0] = items[index1];
            items[index1] = temp;
        }

        protected void AddToEnd(T item)
        {
            ExpandIfNecessary();
            items[Count] = item;
            Count++;
        }

        protected void AddTo(T item, int index)
        {
            items[index] = item;
            if (index + 1 > Count)
            {
                Count = index + 1;
            }
        }

        protected void AddToStart(T item)
        {
            throw new System.NotImplementedException();
        }

        protected void ExpandIfNecessary()
        {
            if (Capacity == 0)
            {
                items = new T[4];
                return;
            }

            if (Count >= Capacity)
            {
                var newItems = new T[items.Length * 2];
                for (int i = 0; i < items.Length; i++)
                {
                    newItems[i] = items[i];
                }
                items = newItems;
            }
        }

        protected void ShiftLeft(int from)
        {
            for (int j = from; j < Count - 1; j++)
            {
                items[j] = items[j + 1];
            }
        }

        protected void ShiftRight()
        {
            throw new System.NotImplementedException();
        }

        protected void Remove(T item)
        {
            var result = Searching.LinearSearch(item, items);
            if (result.found) RemoveFrom(result.index);
        }

        protected void RemoveFrom(int index)
        {
            if (index == Count - 1)
            {
                Count--;
                return;
            }
            ShiftLeft(index);
            Count--;
        }

        protected bool Contains(T item) => Searching.LinearSearch(item, items).found;

        protected int IndexOf(T item)
        {
            var result = Searching.LinearSearch(item, items);
            if (result.found)
            {
                return result.index;
            }
            else
            {
                return -1;
            }
        }

        protected void Clear()
        {
            items = new T[0];
            Count = 0;
        }

        public void Debug()
        {
            if (Count == 0)
            {
                System.Console.WriteLine("\nCollection is empty!");
            }
            else
            {
                System.Console.WriteLine("\nItems:");
                for (int i = 0; i < Count; i++)
                {
                    if (items[i] == null) continue;
                    System.Console.WriteLine($"{i}: {items[i].ToString()}");
                }
            }
            System.Console.WriteLine($"\nLength:   {items.Length}");
            System.Console.WriteLine($"Capacity: {Capacity}");
            System.Console.WriteLine($"Count:    {Count}\n");
        }

    }
}