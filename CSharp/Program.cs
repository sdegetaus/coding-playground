namespace CodingPlayground
{
    class Program
    {
        static void Main(string[] args)
        {

            var dt = new HashTable<string, string>(15);
            dt.Add("txt", "notepad.exe");
            dt.Add("bmp", "paint.exe");
            dt.Add("dib", "paint.exe");
            dt.Add("rtf", "wordpad.exe");
            dt.Add("psd", "photoshop.psd");
            dt.Add("asd", "aha!.psd");

            var t = dt["bmp"];
            System.Console.WriteLine(t);
        }

    }
}
