namespace ConsoleGraphics.Collections
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

        public List() : base() { }

        public List(int capacity) : base(capacity) { }

        public List(params T[] items) : base(items) { }

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

        public void TrimExcess()
        {
            var newItems = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                newItems[i] = items[i];
            }
            items = newItems;
        }

        public void Reverse()
        {
            var newItems = new T[Count];
            for (int i = Count - 1; i >= 0; i--)
            {
                newItems[Count - 1 - i] = items[i];
            }
            items = newItems;
        }

        public override string ToString()
        {
            string result = string.Empty;
            for (int i = 0; i < Count; i++)
            {
                result += items[i].ToString() + (i != Count - 1 ? " " : "");
            }
            return result;
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