namespace CodingPlayground
{
    public static class Searching
    {
        public static SearchResult<T> LinearSearch<T>(T element, T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (element.Equals(array[i]))
                {
                    return new SearchResult<T>(array[i], i, true);
                }
            }
            return new SearchResult<T>(element, 0, false);
        }

    }
}