namespace CodingPlayground
{
    public struct SearchResult<T> : IDebug
    {
        public T value;
        public int index;
        public bool found;

        public SearchResult(T value, int index, bool found)
        {
            this.value = value;
            this.index = index;
            this.found = found;
        }

        public override string ToString()
        {
            if (found)
            {
                return $"Value \"{value}\" found at position {index}";
            }
            else
            {
                return $"Value \"{value}\" not found";
            }
        }

        public void Debug()
        {
            System.Console.WriteLine(this.ToString());
        }

    }
}