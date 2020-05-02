using System;

namespace Console3D
{
    public class ConsoleWindow
    {
        public int width { get; private set; }

        public int height { get; private set; }

        private string windowBaseTitle { get; set; }


        public ConsoleWindow(int width, int height)
        {
            this.width = width;
            this.height = height;

            windowBaseTitle = "Console 3D";
            Console.CursorVisible = false;

            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        public void UpdateTitle(float fps)
        {
            Console.Title = $"{windowBaseTitle} (FPS: {fps.ToString("0.00")})";
        }

    }
}