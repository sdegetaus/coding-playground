using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Console3D
{
    public class Console3D
    {
        private readonly IntPtr stdOutputHandle = NativeMethods.GetStdHandle(-11);
        private readonly IntPtr stdInputHandle = NativeMethods.GetStdHandle(-10);
        private NativeMethods.CharInfo[] CharInfoBuffer { get; set; }
        private SafeFileHandle sFileHandle { get; set; }
        private char[,] charBuffer;
        private int[,] colorBuffer;

        public int width { get; private set; }

        public int height { get; private set; }

        public string title
        {
            get => Console.Title;
            set => Console.Title = value;
        }

        public Console3D(int width, int height)
        {
            this.width = width;
            this.height = height;

            Console.Title = "Default Window";
            Console.CursorVisible = false;

            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(width, height);

            Console.SetBufferSize(width, height);

            charBuffer = new char[width, height];
            colorBuffer = new int[width, height];

            SetFont(stdOutputHandle, 2, 2);

            NativeMethods.SetConsoleMode(stdInputHandle, 0x0080);

            sFileHandle = NativeMethods.CreateFile(
               "CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero
            );

            if (!sFileHandle.IsInvalid)
            {
                CharInfoBuffer = new NativeMethods.CharInfo[this.width * this.height];
            }

            // DrawTriangle(new Vector2(0, 0), new Vector2(5, 0), new Vector2(0, 5), ConsoleColor.Red, ConsoleChar.Full);
            Update();
        }

        public void Update()
        {
            DisplayBuffer();
            Blit();
        }

        public void Clear()
        {
            Array.Clear(charBuffer, 0, charBuffer.Length);
            Array.Clear(colorBuffer, 0, colorBuffer.Length);
        }

        private void DisplayBuffer()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int i = (y * width) + x;
                    CharInfoBuffer[i].Attributes = (short)(colorBuffer[x, y] | (0 << 4));
                    CharInfoBuffer[i].UnicodeChar = charBuffer[x, y];
                }
            }
        }

        private bool Blit()
        {
            var rect = new NativeMethods.SmallRect()
            {
                Left = 0,
                Top = 0,
                Right = (short)width,
                Bottom = (short)height
            };

            return NativeMethods.WriteConsoleOutputW(sFileHandle, CharInfoBuffer,
                new NativeMethods.Coord()
                {
                    X = (short)width,
                    Y = (short)height
                },
                new NativeMethods.Coord()
                {
                    X = 0,
                    Y = 0
                }, ref rect);
        }

        internal static int SetFont(IntPtr handle, short sizeX, short sizeY)
        {
            if (handle == new IntPtr(-1))
            {
                return Marshal.GetLastWin32Error();
            }

            var cfi = new NativeMethods.CONSOLE_FONT_INFO_EX();
            cfi.cbSize = (uint)Marshal.SizeOf(cfi);
            cfi.nFont = 0;
            cfi.dwFontSize.X = sizeX;
            cfi.dwFontSize.Y = sizeY;
            cfi.FaceName = (sizeX < 4 || sizeY < 4) ? "Consolas" : "Terminal";

            NativeMethods.SetCurrentConsoleFontEx(handle, false, ref cfi);
            return 0;
        }

        private void SetPixel(Vector2 p, ConsoleColor cColor, ConsoleChar cChar)
        {
            // check boundaries
            if (p.x >= width || p.y >= height || p.x < 0 || p.y < 0) return;
            charBuffer[(int)p.x, (int)p.y] = (char)cChar;
            colorBuffer[(int)p.x, (int)p.y] = (int)cColor;
        }

        public void DrawLine(
            Vector2 p0, Vector2 p1, ConsoleColor cColor, ConsoleChar cChar)
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

        public void DrawTriangle(
            Vector2 p0, Vector2 p1, Vector2 p2, ConsoleColor cColor, ConsoleChar cChar)
        {
            DrawLine(p0, p1, cColor, cChar);
            DrawLine(p1, p2, cColor, cChar);
            DrawLine(p2, p0, cColor, cChar);
        }

    }
}