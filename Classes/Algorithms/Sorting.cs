namespace CodingPlayground
{
    public static class Sorting
    {
        public static SortResult<int> BubbleSort(int[] sourceArray)
        {
            var array = new int[sourceArray.Length];
            System.Array.Copy(sourceArray, array, sourceArray.Length);

            var count = array.Length;
            var iterations = 0;
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
                    iterations++;
                }
                iterations++;
            }
            return new SortResult<int>("Bubble", iterations, array);
        }

        public static SortResult<int> SelectionSort(int[] sourceArray)
        {
            var array = new int[sourceArray.Length];
            System.Array.Copy(sourceArray, array, sourceArray.Length);

            var iterations = 0;
            var count = array.Length;

            for (int i = 0; i < count - 1; i++)
            {
                var minIndex = i + 1;

                for (int j = i; j < count; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                    iterations++;
                }
                var temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;

                iterations++;
            }
            return new SortResult<int>("Selection", iterations, array);
        }

        public static SortResult<int> InsertionSort(int[] sourceArray)
        {
            var array = new int[sourceArray.Length];
            System.Array.Copy(sourceArray, array, sourceArray.Length);

            var iterations = 0;
            var count = array.Length;

            for (int i = 1; i < count; i++)
            {
                int j = i - 1;
                int current = array[i];

                while (j >= 0 && array[j] > current)
                {
                    array[j + 1] = array[j];
                    j--;
                    iterations++;
                }
                array[j + 1] = current;
                iterations++;
            }

            return new SortResult<int>("Insertion", iterations, array);
        }

    }
}