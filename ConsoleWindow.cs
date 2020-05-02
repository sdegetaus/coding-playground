using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Console3D
{
    public class ConsoleWindow
    {
        public int width
        {
            get => Console.WindowWidth;
            private set
            {
                Console.WindowWidth = value;
            }
        }

        public int height
        {
            get => Console.WindowHeight;
            private set
            {
                Console.WindowHeight = value;
            }
        }

        public string title
        {
            get => Console.Title;
            set => Console.Title = value;
        }

        private PixelBuffer pixelBuffer;

        private readonly IntPtr stdInputHandle = NativeMethods.GetStdHandle(-10);


        #region Constructors

        public ConsoleWindow(int width, int height)
        {
            Console.Title = "Default Window";
            Console.CursorVisible = false;

            // set window position and size
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(width, height);

            // set buffers sizes
            Console.SetBufferSize(width, height);
            pixelBuffer = new PixelBuffer(width, height);

            NativeMethods.SetConsoleMode(stdInputHandle, 0x0080);

            Display();
        }

        #endregion


        public void Display()
        {
            pixelBuffer.Set();
            pixelBuffer.Blit();
        }

        public void Clear()
        {
            pixelBuffer.Clear();
        }



















        // TODO: MOVE DRAW METHODS

        private void SetPixel(Vector2 p, ConsoleColor cColor, ConsoleChar cChar)
        {
            if (p.x >= width ||
                p.y >= height ||
                p.x < 0 ||
                p.y < 0)
            {
                return;
            }

            pixelBuffer[(int)p.x, (int)p.y] = (cColor, cChar);

        }


        public void DrawTriangle(
            Vector2 p0, Vector2 p1, Vector2 p2, ConsoleColor cColor, ConsoleChar cChar)
        {
            DrawLine(p0, p1, cColor, cChar);
            DrawLine(p1, p2, cColor, cChar);
            DrawLine(p2, p0, cColor, cChar);
        }

        public void DrawLine(Vector2 p0, Vector2 p1, ConsoleColor cColor, ConsoleChar cChar)
        {
            var delta = p1 - p0;
            Vector2 da = Vector2.zero;
            Vector2 db = Vector2.zero;

            if (delta.x < 0) da.x = -1; else if (delta.x > 0) da.x = 1;
            if (delta.y < 0) da.y = -1; else if (delta.y > 0) da.y = 1;
            if (delta.x < 0) db.x = -1; else if (delta.x > 0) db.x = 1;
            int longest = Math.Abs((int)delta.x);
            int shortest = Math.Abs((int)delta.y);

            if (!(longest > shortest))
            {
                longest = Math.Abs((int)delta.y);
                shortest = Math.Abs((int)delta.x);
                if (delta.y < 0) db.y = -1; else if (delta.y > 0) db.y = 1;
                db.x = 0;
            }

            int numerator = longest >> 1;
            var p = new Vector2(p0.x, p0.y);
            for (int i = 0; i <= longest; i++)
            {
                SetPixel(p, cColor, cChar);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    p += da;
                }
                else
                {
                    p += db;
                }
            }
        }

    }
}