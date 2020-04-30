using CodingPlayground;

public class Program
{
    static void Main(string[] args)
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        var path = System.IO.Path.Combine(
            @"C:\Users\minim\Desktop\image_output",
            "output.bmp"
        );

        Bitmap bitmap = new Bitmap(
            width: 64,
            height: 64
        );

        bitmap.Fill(Color.blue);
        bitmap.DrawRectangle(0, 0, 64, 20, new Gradient(Color.black, Color.white));

        bitmap.Save(path);

        stopwatch.Stop();
        System.Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
    }

}
