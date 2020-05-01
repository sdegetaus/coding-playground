using System;

namespace Console3D
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.CursorVisible = false;
            Console.Title = "Console 3D Engine";
            System.Console.WriteLine(Console.LargestWindowWidth);
            System.Console.WriteLine(Console.LargestWindowHeight);
            Console.BufferWidth = 400;
            Console.BufferHeight = 400;
            Console.SetWindowSize(100, 100);
        }
    }
}
