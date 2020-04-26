namespace CodingPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 5, 1, 4, 2, 8 };
            BubbleSort.Sort(array);
            System.Console.WriteLine(string.Join(", ", array));
        }

    }
}
