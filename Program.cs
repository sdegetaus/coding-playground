using System;
using System.Diagnostics;

namespace ConsoleGraphics
{
    public class Program
    {
        private static int WIDTH = 256;
        private static int HEIGHT = 256;

        private static Stopwatch stopwatch;

        private static ConsoleBuffer consoleBuffer;

        static void Main(string[] args)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                Debug.Log("Program started");

                consoleBuffer = new ConsoleBuffer(WIDTH, HEIGHT);
                consoleBuffer.DrawLine(new Vector2(0, 0), new Vector2(256, 256), Color.white);
                consoleBuffer.Blit();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

            stopwatch.Stop();
            Debug.Log($"Time taken: {stopwatch.Elapsed}");

            Console.Read();

        }

    }
}