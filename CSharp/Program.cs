using CodingPlayground;
using System;

public struct Vector2
{
    public int x;
    public int y;

    public Vector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        string path = System.IO.Path.Combine(@"/Users/taus/Desktop/output/output.bmp");
        Bitmap bitmap = new Bitmap(32, 32);
        bitmap.Fill(Color.black);

        var cursor = new Vector2(-1, -1);

        while (true)
        {
            var keyInfo = Console.ReadKey();
            System.Console.WriteLine();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    cursor.y++;
                    break;
                case ConsoleKey.RightArrow:
                    cursor.x++;
                    break;
                case ConsoleKey.DownArrow:
                    cursor.y--;
                    break;
                case ConsoleKey.LeftArrow:
                    cursor.x--;
                    break;
                case ConsoleKey.R:
                    bitmap.Fill(Color.red);
                    break;

                case ConsoleKey.G:
                    bitmap.Fill(Color.green);
                    break;

                case ConsoleKey.S:
                    System.Console.WriteLine("Saved");
                    break;
            }

            bitmap.SetPixel(cursor.x, cursor.y, Color.green);
            bitmap.Save(path);
        }
    }
}
