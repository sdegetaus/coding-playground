namespace CodingPlayground
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] data = { 3, 8, 1, 5, 90, 12, 876, 12 };
            var result = Sorting.BubbleSort(data);
            System.Console.WriteLine(result);
            System.Console.WriteLine(string.Join(", ", data));

        }

    }
}
