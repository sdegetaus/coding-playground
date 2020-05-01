namespace Console3D.Standard
{
    public class Dictionary<TKey, TValue> : Collection<KeyValuePair<TKey, TValue>>
    {
        public TValue this[TKey key]
        {
            get
            {
                int index = IndexOf(key);
                if (index == -1)
                {
                    throw new System.NullReferenceException();
                }

                return items[index].value;
            }
            set
            {
                int index = IndexOf(key);
                if (index == -1)
                {
                    throw new System.NullReferenceException();
                }
                items[index].value = value;
            }
        }

        public Dictionary() : base() { }

        public Dictionary(int capacity) : base(capacity) { }

        public Dictionary(params KeyValuePair<TKey, TValue>[] items) : base(items) { }

        public void Add(TKey key, TValue value)
        {
            AddToEnd(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Remove(TKey key)
        {
            int index = IndexOf(key);
            if (index == -1) return;
            base.RemoveFrom(index);
        }

        public new void Clear() => base.Clear();

        public bool ContainsKey(TKey key) => IndexOf(key) != -1;

        private int IndexOf(TKey key)
        {
            for (int i = 0; i < Count; i++)
            {
                if (key.Equals(items[i].key))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}