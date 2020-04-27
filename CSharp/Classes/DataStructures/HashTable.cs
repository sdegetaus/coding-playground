namespace CodingPlayground
{
    public class HashTable<TKey, TValue> : Collection<LinkedList<KeyValuePair<TKey, TValue>>>
    {
        public TValue this[TKey key]
        {
            get
            {
                var result = Find(key);
                if (!result.found)
                {
                    throw new System.NullReferenceException();
                }
                return result.value;
            }

            set
            {
                var result = Find(key);
                if (!result.found)
                {
                    throw new System.NullReferenceException();
                }

                var node = items[result.index].First;
                while (node != null)
                {
                    if (node.value.value.Equals(result.value))
                    {
                        node.value.value = value;
                        return;
                    }
                    node = node.next;
                }

                throw new System.NullReferenceException();
            }
        }

        public HashTable(int capacity) : base(capacity)
        {
            if (items.Length == 0)
            {
                throw new System.ArgumentException(
                    "Capacity can't be less or equal to zero"
                );
            }
        }

        public void Add(TKey key, TValue value)
        {
            var item = new KeyValuePair<TKey, TValue>(key, value);
            int index = IndexOf(key);

            if (items[index] != null)
            {
                if (Contains(key))
                {
                    throw new System.InvalidOperationException(
                        $"The key \"{key}\" already exists in Collection."
                    );
                }

                items[index].AddLast(item);
                Count++;
            }
            else
            {
                AddTo(new LinkedList<KeyValuePair<TKey, TValue>>(item), index);
            }
        }

        public bool Contains(TKey key) => Find(key).found;

        public SearchResult<TValue> Find(TKey key)
        {
            int index = IndexOf(key);
            if (items[index] == null)
            {
                throw new System.NullReferenceException();
            }

            var list = items[index];
            var node = list.First;

            while (node != null)
            {
                if (key.Equals(node.value.key))
                {
                    return new SearchResult<TValue>(node.value.value, index, true);
                }
                node = node.next;
            }

            return new SearchResult<TValue>(default, -1, false);
        }

        private int IndexOf(TKey key)
        {
            if (items.Length <= 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            int sum = 0;
            byte[] asciiBytes = System.Text.Encoding.ASCII.GetBytes(key.ToString());
            for (int i = 0; i < asciiBytes.Length; i++) sum += asciiBytes[i];
            return sum % items.Length;
        }

        public override void Debug()
        {
            if (Count == 0)
            {
                System.Console.WriteLine("\nCollection is empty!");
            }
            else
            {
                System.Console.WriteLine("\nItems:");
                for (int i = 0; i < items.Length; i++)
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