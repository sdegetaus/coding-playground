namespace CodingPlayground
{
    public class List<T> : Collection<T>
    {
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                return items[index];
            }

            set
            {
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

        public List(params T[] items)
        {
            this.items = items;
            Count = items.Length;
        }

        public void Add(params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                AddToEnd(items[i]);
            }
        }

        public void Remove(params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                base.Remove(items[i]);
            }
        }

        public new void Clear()
        {
            base.Clear();
        }

        public void ForEach(Action<T> callback)
        {
            for (int i = 0; i < Count; i++)
            {
                callback.Invoke(items[i]);
            }
        }

        public List<T> Map(Func<T, int, T> callback)
        {
            var newList = Clone() as List<T>;
            for (int i = 0; i < Count; i++)
            {
                newList[i] = callback.Invoke(items[i], i);
            }
            return newList;
        }

    }
}