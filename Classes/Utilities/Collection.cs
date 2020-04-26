namespace DataStructures.Utilities
{
    public abstract class Collection<T>
    {
        protected T[] items;

        public int Capacity { get => items.Length; }

        public int Count { get; protected set; }

        public T First { get => items[0]; }

        public T Last { get => items[Count - 1]; }

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
            if (Capacity == 0) this.items = new T[4];

        }

        protected void ShiftLeft(int from)
        {
            for (int j = from; j < Count - 1; j++)
            {
                items[j] = items[j + 1];
            }
        }

        private void ShiftRight()
        {
            throw new System.NotImplementedException();
        }

        protected void Remove(T item)
        {
            bool found = false;
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(items[i]))
                {
                    found = true;
                    if (i == Count - 1) break;
                    ShiftLeft(from: i);
                    break;
                }
            }

            if (found) Count--;
        }

        public void Debug()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Items:");
            for (int i = 0; i < Count; i++)
            {
                System.Console.WriteLine($"{i}: {items[i]}");
            }
            System.Console.WriteLine();
            System.Console.WriteLine($"Length:   {items.Length}");
            System.Console.WriteLine($"Capacity: {Capacity}");
            System.Console.WriteLine($"Count:    {Count}");
            System.Console.WriteLine();
        }

    }
}