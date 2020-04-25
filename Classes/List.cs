using DataStructures.Utilities;

namespace DataStructures
{
    public class List<T> : Collection<T>
    {
        public override T this[int index]
        {
            get
            {
                if (index == -1) return items[Count - 1];
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
            if (Capacity == 0) this.items = new T[4];

            for (int i = 0; i < items.Length; i++)
            {
                if (Count == Capacity)
                {
                    var _items = new T[this.items.Length * 2];
                    for (int j = 0; j < this.items.Length; j++)
                    {
                        _items[j] = this.items[j];
                    }
                    this.items = _items;
                }

                this.items[Count] = items[i];
                Count++;
            }
        }

        public void Remove(T item)
        {
            bool found = false;
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(items[i]))
                {
                    if (i == Count - 1)
                    {
                        found = true;
                        break;
                    }

                    // shift left
                    for (int j = i; j < Count - 1; j++)
                    {
                        items[j] = items[j + 1];
                        found = true;
                    }

                    break;
                }
            }

            if (found)
            {
                RemoveLast();
            }
        }

        public void RemoveLast()
        {
            Count--;
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