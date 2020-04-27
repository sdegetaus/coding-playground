namespace CodingPlayground
{
    public class HashTable<TKey, TValue> : Collection<LinkedList<KeyValuePair<TKey, TValue>>>
    {
        public TValue this[TKey key]
        {
            get
            {
                var result = Find(key);
                if (result.found == false)
                {
                    throw new System.NullReferenceException();
                }
                return result.value;
            }
        }

        public HashTable() : base() { }

        public HashTable(int capacity) : base(capacity) { }

        public void Add(TKey key, TValue value)
        {
            var item = new KeyValuePair<TKey, TValue>(key, value);
            int index = IndexOf(key);

            if (items[index] != null)
            {
                items[index].AddLast(item);
            }
            else
            {
                AddTo(new LinkedList<KeyValuePair<TKey, TValue>>(item), index);
            }
        }

        public SearchResult<TValue> Find(TKey key)
        {
            int index = IndexOf(key);
            if (items[index] == null)
            {
                throw new System.NullReferenceException();
            }

            var list = items[index];
            var node = list.First;
            int count = 0;

            while (node != null)
            {
                if (key.Equals(node.value.key))
                {
                    return new SearchResult<TValue>(node.value.value, count, true);
                }
                node = node.next;
                count++;
            }

            return new SearchResult<TValue>(default, -1, false);
        }

        private int IndexOf(TKey key)
        {
            int sum = 0;
            byte[] asciiBytes = System.Text.Encoding.ASCII.GetBytes(key.ToString());
            for (int i = 0; i < asciiBytes.Length; i++) sum += asciiBytes[i];
            return sum % items.Length;
        }

    }
}