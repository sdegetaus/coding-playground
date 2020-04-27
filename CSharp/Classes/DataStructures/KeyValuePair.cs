namespace CodingPlayground
{
    public class KeyValuePair<TKey, TValue> : IDebug
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

        public void Debug()
        {
            System.Console.WriteLine(this.ToString());
        }

    }
}