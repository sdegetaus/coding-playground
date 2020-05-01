namespace Console3D
{
    public struct SortResult<T>
    {
        ///<summary>
        /// Sorting technique name.
        ///</summary>
        public string name;

        ///<summary>
        /// Iterations taken to complete sort.
        ///</summary>
        public int iterations;

        ///<summary>
        /// Sorted array.
        ///</summary>
        public T[] result;

        public SortResult(string name, int iterations, T[] result)
        {
            this.name = name;
            this.iterations = iterations;
            this.result = result;
        }

        public override string ToString() =>
            $"Sorting \"{name}\" took {iterations} iterations => [ {string.Join(", ", result)} ]";
    }
}