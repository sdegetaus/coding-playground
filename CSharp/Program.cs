namespace CodingPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new List<int>(2, 5, 8, 12, 16, 23, 38, 56, 72, 20, 91);
            var r0 = Searching.LinearSearch<int>(25, arr.ToArray());
            var r1 = Searching.LinearSearch<int>(91, arr.ToArray());
            System.Console.WriteLine(r0);
            System.Console.WriteLine(r1);
        }

    }
}
