namespace CodingPlayground
{
    public struct SearchResult<T> : IDebug
    {
        ///<summary>
        /// Search value
        ///</summary>
        public T value;

        ///<summary>
        /// The index of the element.
        /// Returns -1 if the element was not found.
        ///</summary>
        public int index;

        ///<summary>
        /// True when the element was found, false otherwise.
        ///</summary>
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