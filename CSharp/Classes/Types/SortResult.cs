namespace CodingPlayground
{
    public struct SortResult
    {
        public int iterations;

        public SortResult(int iterations)
        {
            this.iterations = iterations;
        }

        public override string ToString()
        {
            return $"Sorting took {iterations} iterations.";
        }

    }
}