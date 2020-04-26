namespace DataStructures.Utilities
{
    public class KeyValuePair<TKey, TValue>
    {
        public TKey key;
        public TValue value;

        public KeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return $"{key.ToString()}, {value.ToString()}";
        }
    }
}