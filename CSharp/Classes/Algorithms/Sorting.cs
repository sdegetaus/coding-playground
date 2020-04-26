namespace CodingPlayground
{
    public static class Sorting
    {
        public static void BubbleSort(int[] array)
        {
            var count = array.Length;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count - 1 - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

    }
}