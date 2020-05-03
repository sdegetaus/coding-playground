using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ConsoleGraphics
{
    public class ConsoleBuffer
    {
        private Native.CHAR_INFO[] CharInfoBuffer { get; set; }
        private SafeFileHandle sFileHandle { get; set; }
        private ConsoleWindow consoleWindow;
        private int width;
        private int height;

        public ConsoleBuffer(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.consoleWindow = new ConsoleWindow(width, height);

            SetFont(Native.stdOutputHandle, 2, 2);

            sFileHandle = Native.CreateFile(
               "CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero
            );

            if (!sFileHandle.IsInvalid)
            {
                CharInfoBuffer = new Native.CHAR_INFO[width * height];
            }

            Native.SetConsoleMode(Native.stdInputHandle, 0x0080);
        }

        public void ClearBuffer()
        {
            CharInfoBuffer = new Native.CHAR_INFO[width * height];
        }

        public bool Blit()
        {
            var rect = new Native.SMALL_RECT()
            {
                Left = 0,
                Top = 0,
                Right = (short)width,
                Bottom = (short)height
            };

            return Native.WriteConsoleOutputW(
                sFileHandle,
                CharInfoBuffer,
                new Native.COORD()
                {
                    X = (short)width,
                    Y = (short)height
                },
                new Native.COORD()
                {
                    X = 0,
                    Y = 0
                },
                ref rect
            );
        }

        internal static int SetFont(IntPtr handle, short sizeX, short sizeY)
        {
            if (handle == new IntPtr(-1))
            {
                return Marshal.GetLastWin32Error();
            }

            var cfi = new Native.CONSOLE_FONT_INFO_EX();
            cfi.cbSize = (uint)Marshal.SizeOf(cfi);
            cfi.nFont = 0;
            cfi.dwFontSize.X = sizeX;
            cfi.dwFontSize.Y = sizeY;
            cfi.FaceName = (sizeX < 4 || sizeY < 4) ? "Consolas" : "Terminal";

            Native.SetCurrentConsoleFontEx(handle, false, ref cfi);
            return 0;
        }

        public void SetPixel(Vector2 p, Color color)
        {
            if (p.x >= width || p.y >= height || p.x < 0 || p.y < 0) return;
            int i = p.y * width + p.x;

            var cColor = color.ToCColor();

            CharInfoBuffer[i].Attributes = (short)((int)cColor.fg | ((int)cColor.bg << 4));
            CharInfoBuffer[i].UnicodeChar = (char)cColor.sym;
        }

        public void DrawLine(
            Vector2 p0, Vector2 p1, Color c)
        {
            var delta = p1 - p0;
            Vector2 da = Vector2.zero;
            Vector2 db = Vector2.zero;

            if (delta.x < 0) da.x = -1; else if (delta.x > 0) da.x = 1;
            if (delta.y < 0) da.y = -1; else if (delta.y > 0) da.y = 1;
            if (delta.x < 0) db.x = -1; else if (delta.x > 0) db.x = 1;
            int longest = Math.Abs(delta.x);
            int shortest = Math.Abs(delta.y);

            if (longest > shortest == false)
            {
                longest = Math.Abs(delta.y);
                shortest = Math.Abs(delta.x);
                if (delta.y < 0) db.y = -1; else if (delta.y > 0) db.y = 1;
                db.x = 0;
            }

            int numerator = longest >> 1;
            var p = new Vector2(p0.x, p0.y);
            for (int i = 0; i <= longest; i++)
            {
                SetPixel(p, c);
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
            Vector2 p0, Vector2 p1, Vector2 p2, Color c)
        {
            DrawLine(p0, p1, c);
            DrawLine(p1, p2, c);
            DrawLine(p2, p0, c);
        }

        public void FillTriangle(
            Vector2 a, Vector2 b, Vector2 c, Color color)
        {
            var min = new Vector2(Math.Min(Math.Min(a.x, b.x), c.x), Math.Min(Math.Min(a.y, b.y), c.y));
            var max = new Vector2(Math.Max(Math.Max(a.x, b.x), c.x), Math.Max(Math.Max(a.y, b.y), c.y));

            var p = Vector2.zero;
            for (p.y = min.y; p.y < max.y; p.y++)
            {
                for (p.x = min.x; p.x < max.x; p.x++)
                {
                    int w0 = Orient(b, c, p);
                    int w1 = Orient(c, a, p);
                    int w2 = Orient(a, b, p);

                    if (w0 >= 0 && w1 >= 0 && w2 >= 0)
                    {
                        SetPixel(p, color);
                    }
                }
            }
        }

        private int Orient(Vector2 a, Vector2 b, Vector2 c)
        {
            return ((b.x - a.x) * (c.y - a.y)) - ((b.y - a.y) * (c.x - a.x));
        }

    }
}