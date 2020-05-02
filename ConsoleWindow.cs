using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Console3D
{
    public enum CharType
    {
        Null = 0x0000,
        Full = 0x2588,
        Dark = 0x2593,
        Medium = 0x2592,
        Light = 0x2591,
    }

    public class ConsoleWindow
    {
        #region Properties

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

        public ConsoleColor BackgroundColor
        {
            get => Console.BackgroundColor;
            private set => Console.BackgroundColor = value;
        }

        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            private set => Console.ForegroundColor = value;
        }

        public string Title
        {
            get => Console.Title;
            set => Console.Title = value;
        }

        #endregion

        #region Constructors

        public ConsoleWindow(int width, int height)
        {
            Console.Title = "Default Window";
            Console.CursorVisible = false;

            Console.SetWindowPosition(0, 0);

            Debug.Log(Console.LargestWindowWidth);
            Debug.Log(Console.LargestWindowHeight);

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            // --------------------------------------------------------

            NativeMethods.SetConsoleMode(stdInputHandle, 0x0080);

            ConsoleBuffer = new ConsoleBuffer(width, height);

            ColorBuffer = new int[width, height];
            BackgroundBuffer = new int[width, height];
            CharBuffer = new char[width, height];

            DisplayBuffer();

            SetFont(stdOutputHandle, 2, 2);
        }

        #endregion
        private char[,] CharBuffer { get; set; }
        private int[,] ColorBuffer { get; set; }
        private int[,] BackgroundBuffer { get; set; }
        private int Background { get; set; }
        private ConsoleBuffer ConsoleBuffer { get; set; }
        private readonly IntPtr stdInputHandle = NativeMethods.GetStdHandle(-10);
        private readonly IntPtr stdOutputHandle = NativeMethods.GetStdHandle(-11);

        public void DisplayBuffer()
        {
            ConsoleBuffer.SetBuffer(CharBuffer, ColorBuffer, BackgroundBuffer, ConsoleColor.Black);
            ConsoleBuffer.Blit();
        }

        public void ClearBuffer()
        {
            Array.Clear(CharBuffer, 0, CharBuffer.Length);
            Array.Clear(ColorBuffer, 0, ColorBuffer.Length);
            // Array.Clear(BackgroundBuffer, 0, BackgroundBuffer.Length);
        }

        // temp
        internal static int SetFont(IntPtr h, short sizeX, short sizeY)
        {
            if (h == new IntPtr(-1)) return Marshal.GetLastWin32Error();

            var cfi = new NativeMethods.CONSOLE_FONT_INFO_EX();
            cfi.cbSize = (uint)Marshal.SizeOf(cfi);
            cfi.nFont = 0;

            cfi.dwFontSize.x = sizeX;
            cfi.dwFontSize.Y = sizeY;

            if (sizeX < 4 || sizeY < 4) cfi.FaceName = "Consolas";
            else cfi.FaceName = "Terminal";

            NativeMethods.SetCurrentConsoleFontEx(h, false, ref cfi);
            return 0;
        }


















        private void SetPixel(Vector2 pos, ConsoleColor color, CharType chType)
        {
            if (pos.x >= CharBuffer.GetLength(0) ||
                pos.y >= CharBuffer.GetLength(1) ||
                pos.x < 0 ||
                pos.y < 0)
            {
                return;
            }
            CharBuffer[(int)pos.x, (int)pos.y] = (char)chType;
            ColorBuffer[(int)pos.x, (int)pos.y] = (int)color;
        }


        public void DrawLine(
            Vector2 pos1, Vector2 pos2, ConsoleColor color, CharType chType)
        {
            var factor = Vector2.Distance(pos1, pos2);
            for (int i = 0; i < factor; i++)
            {
                var lerp = Vector2.Lerp(pos1, pos2, (1f / factor) * i);
                var finalPos = lerp;

                if (finalPos.x < 0 ||
                    finalPos.y < 0 ||
                    finalPos.x >= width ||
                    finalPos.y >= height)
                {
                    continue;
                }
                SetPixel(finalPos, color, chType);
            }
        }

        public void DrawTriangle(
            Vector2 p0, Vector2 p1, Vector2 p2, ConsoleColor color, CharType chType)
        {
            // DrawLine(p0, p1, color, chType);
            // DrawLine(p1, p2, color, chType);
            // DrawLine(p2, p0, color, chType);

            Line(p0, p1, color, chType);
            Line(p1, p2, color, chType);
            Line(p2, p0, color, chType);

        }

        public void Line(Vector2 start, Vector2 end, ConsoleColor color, CharType c = CharType.Full)
        {
            var delta = end - start;
            Vector2 da = Vector2.zero, db = Vector2.zero;

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
            var p = new Vector2(start.x, start.y);
            for (int i = 0; i <= longest; i++)
            {
                SetPixel(p, color, c);
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