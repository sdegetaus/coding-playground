using DataStructures.Utilities;

namespace DataStructures
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

        public Dictionary() : this(0) { }

        public Dictionary(int capacity)
        {
            if (capacity < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            items = new KeyValuePair<TKey, TValue>[capacity];
        }

        public void Add(TKey key, TValue value)
        {
            AddToEnd(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Remove(TKey key)
        {
            int index = IndexOf(key);
            if (index == -1) return;
            base.RemoveAt(index);
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