using System;

namespace CodingPlayground
{
    public class Input
    {
        public void T()
        {
            string path = System.IO.Path.Combine(@"/Users/taus/Desktop/output/output.bmp");
            Bitmap bitmap = new Bitmap(64, 64);
            bitmap.Fill(Color.white);
            Console.WriteLine("Reading input...");
            var cursor = new Vector2(0, 0);

            while (true)
            {
                var keyInfo = Console.ReadKey();
                bitmap.SetPixel(cursor.x, cursor.y, Color.black);

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
                    case ConsoleKey.Escape:
                        bitmap.Save(path);
                        return;
                }

                bitmap.Save(path);
            }
        }
    }
}