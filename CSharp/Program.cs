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
            width: 256,
            height: 256
        );

        // bitmap.PerlinNoise(8, 1.0f);
        bitmap.Noise();
        bitmap.Posterize(3);

        bitmap.Save(path);

        stopwatch.Stop();
        System.Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
    }

}
