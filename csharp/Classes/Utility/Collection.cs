namespace CodingPlayground.Utility
{
    public abstract class Collection<T>
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

        protected void Swap(int index0, int index1)
        {
            T temp = items[index0];
            items[index0] = items[index1];
            items[index1] = temp;
        }

        protected void AddToEnd(T item)
        {
            if (Capacity == 0) this.items = new T[4];
            if (Count == Capacity)
            {
                var _items = new T[this.items.Length * 2];
                for (int j = 0; j < this.items.Length; j++)
                {
                    _items[j] = this.items[j];
                }
                this.items = _items;
            }
            this.items[Count] = item;
            Count++;
        }

        protected void AddToStart(T item)
        {
            throw new System.NotImplementedException();
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
            int index = IndexOf(item);
            if (index == -1) return;
            RemoveAt(index);
        }

        protected void RemoveAt(int index)
        {
            if (index == Count - 1)
            {
                Count--;
                return;
            }
            ShiftLeft(index);
            Count--;
        }

        protected bool Contains(T item) => IndexOf(item) != -1;

        protected int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(items[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        protected void Clear()
        {
            items = new T[0];
            Count = 0;
        }

        public void Debug()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Items:");
            for (int i = 0; i < Count; i++)
            {
                System.Console.WriteLine($"{i}: {items[i].ToString()}");
            }
            System.Console.WriteLine();
            System.Console.WriteLine($"Length:   {items.Length}");
            System.Console.WriteLine($"Capacity: {Capacity}");
            System.Console.WriteLine($"Count:    {Count}");
            System.Console.WriteLine();
        }

    }
}