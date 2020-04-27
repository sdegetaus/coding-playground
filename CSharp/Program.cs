namespace CodingPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            // System.Random rand = new System.Random();
            // List<int> list = new List<int>(500);
            // for (int i = 0; i < list.Capacity; i++) list.Add(rand.Next(0, 1001));
            // var data = list.ToArray();

            int[] data = { 64, 25, 12, 22, 11, 1 };
            // System.Console.WriteLine($"Original => [ {string.Join(", ", data)} ]");

            var bubble = Sorting.BubbleSort(data);
            var selection = Sorting.SelectionSort(data);
            var insertion = Sorting.InsertionSort(data);

            var resultList = new List<SortResult<int>>(bubble, selection, insertion);
            resultList.ForEach((e) =>
            {
                System.Console.WriteLine($"{e.name} => {e.iterations}");
            });

        }

    }
}
