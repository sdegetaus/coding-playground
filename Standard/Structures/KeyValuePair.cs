namespace Console3D
{
    public struct KeyValuePair<TKey, TValue>
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
            return $"k: {key.ToString()}, v: {value.ToString()}";
        }

        public void Debug()
        {
            System.Console.WriteLine(this.ToString());
        }

    }
}