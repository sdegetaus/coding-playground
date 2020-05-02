using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Console3D
{
    public class Console3D
    {
        private readonly IntPtr stdOutputHandle = Native.GetStdHandle(-11);
        private readonly IntPtr stdInputHandle = Native.GetStdHandle(-10);
        private Native.CharInfo[] CharInfoBuffer { get; set; }
        private SafeFileHandle sFileHandle { get; set; }
        private ConsoleColor backgroundColor { get; set; }
        private ConsoleWindow cWin;

        public Console3D(ConsoleWindow window)
        {
            this.cWin = window;

            backgroundColor = ConsoleColor.DarkRed;

            SetFont(stdOutputHandle, 2, 2);

            sFileHandle = Native.CreateFile(
               "CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero
            );

            if (!sFileHandle.IsInvalid)
            {
                CharInfoBuffer = new Native.CharInfo[window.width * window.height];
            }

            Native.SetConsoleMode(stdInputHandle, 0x0080);

            // FillTriangle(new Vector2(0, 0), new Vector2(25, 0), new Vector2(0, 25), ConsoleColor.Red, ConsoleChar.Light);
            // DrawTriangle(new Vector2(0, 0), new Vector2(25, 0), new Vector2(0, 25), ConsoleColor.Red, ConsoleChar.Full);
        }

        public void ClearBuffer()
        {
            CharInfoBuffer = new Native.CharInfo[cWin.width * cWin.height];
        }

        public bool Blit()
        {
            var rect = new Native.SmallRect()
            {
                Left = 0,
                Top = 0,
                Right = (short)cWin.width,
                Bottom = (short)cWin.height
            };

            return Native.WriteConsoleOutputW(sFileHandle, CharInfoBuffer,
                new Native.Coord()
                {
                    X = (short)cWin.width,
                    Y = (short)cWin.height
                },
                new Native.Coord()
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
            // check boundaries
            if (p.x >= cWin.width || p.y >= cWin.height || p.x < 0 || p.y < 0) return;
            int i = p.y * cWin.width + p.x;

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
            Vector2 p0, Vector2 p1, Vector2 p2, Color c)
        {
            // todo: improve
            int x0 = p0.x;
            int y0 = p0.y;
            int x1 = p1.x;
            int y1 = p1.y;
            int x2 = p2.x;
            int y2 = p2.y;

            float area = 0.5f * (-y1 * x2 + y0 * (-x1 + x2) + x0 * (y1 - y2) + x1 * y2);

            for (int _y = 0; _y < cWin.height; _y++)
            {
                for (int _x = 0; _x < cWin.width; _x++)
                {
                    Vector2 pos = new Vector2(_x, _y);

                    if (pos.x < 0 ||
                        pos.y < 0 ||
                        pos.x >= cWin.width ||
                        pos.y >= cWin.height)
                    {
                        continue;
                    }

                    float s = 1f / (2f * area) * (y0 * x2 - x0 * y2 + (y2 - y0) * pos.x + (x0 - x2) * pos.y);
                    float t = 1f / (2f * area) * (x0 * y1 - y0 * x1 + (y0 - y1) * pos.x + (x1 - x0) * pos.y);

                    if (s > 0f && t > 0f && 1f - s - t > 0f)
                    {
                        SetPixel(pos, c);
                    }
                    else { continue; }
                }
            }

            // Vector2 min = new Vector2(System.Math.Min(System.Math.Min(a.x, b.x), c.x), System.Math.Min(System.Math.Min(a.y, b.y), c.y));
            // Vector2 max = new Vector2(System.Math.Max(System.Math.Max(a.x, b.x), c.x), System.Math.Max(System.Math.Max(a.y, b.y), c.y));

            // var p = Vector2.zero;
            // for (p.y = min.y; p.y < max.y; p.y++)
            // {
            //     for (p.x = min.x; p.x < max.x; p.x++)
            //     {
            //         int w0 = Orient(b, c, p);
            //         int w1 = Orient(c, a, p);
            //         int w2 = Orient(a, b, p);

            //         if (w0 >= 0 && w1 >= 0 && w2 >= 0)
            //         {
            //             SetPixel(p, cColor, cChar);
            //         }
            //     }
            // }
        }

        private int Orient(Vector2 a, Vector2 b, Vector2 c)
        {
            return ((b.x - a.x) * (c.y - a.y)) - ((b.y - a.y) * (c.x - a.x));
        }

    }
}