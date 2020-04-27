namespace CodingPlayground
{
    class Program
    {

        static void Main(string[] args)
        {
            System.Random rand = new System.Random();
            // List<int> x = new List<int>(500);

            // for (int i = 0; i < x.Capacity; i++)
            // {
            //     x.Add(rand.Next(0, 1001));
            // }

            // var data = x.ToArray();
            int[] data = { 64, 25, 12, 22, 11, 1 };
            System.Console.WriteLine($"Original => [ {string.Join(", ", data)} ]");

            var bubble = Sorting.BubbleSort(data);
            System.Console.WriteLine(bubble);

            var selection = Sorting.SelectionSort(data);
            System.Console.WriteLine(selection);

            var insertion = Sorting.InsertionSort(data);
            System.Console.WriteLine(insertion);

            // var l = new List<SortResult<int>>(bubble, selection, insertion);
            // l.

        }

    }
}
