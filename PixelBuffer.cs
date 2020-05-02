using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Console3D
{
    public class PixelBuffer
    {
        private readonly IntPtr stdOutputHandle = NativeMethods.GetStdHandle(-11);
        private NativeMethods.CharInfo[] CharInfoBuffer;
        private SafeFileHandle sFileHandle;

        private char[,] charBuffer;
        private int[,] colorBuffer;
        private readonly int width;
        private readonly int height;

        public (ConsoleColor cColor, ConsoleChar cChar) this[int x, int y]
        {
            get
            {
                return ((ConsoleColor)colorBuffer[x, y], (ConsoleChar)(int)charBuffer[x, y]);
            }
            set
            {
                colorBuffer[x, y] = (int)value.cColor;
                charBuffer[x, y] = (char)value.cChar;
            }
        }

        public PixelBuffer(int width, int height)
        {
            this.width = width;
            this.height = height;

            sFileHandle = NativeMethods.CreateFile(
                "CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero
            );

            if (!sFileHandle.IsInvalid)
            {
                CharInfoBuffer = new NativeMethods.CharInfo[this.width * this.height];
            }

            charBuffer = new char[width, height];
            colorBuffer = new int[width, height];

            SetFont(stdOutputHandle, 2, 2);
        }

        public void Set()
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

        public bool Blit()
        {
            var rect = new NativeMethods.SmallRect()
            {
                Left = 0,
                Top = 0,
                Right = (short)width,
                Bottom = (short)height
            };
            return NativeMethods.WriteConsoleOutputW(
                sFileHandle, CharInfoBuffer,
                new NativeMethods.Coord()
                {
                    x = (short)width,
                    Y = (short)height
                },
                new NativeMethods.Coord()
                {
                    x = 0,
                    Y = 0
                }, ref rect
            );
        }

        public void Clear()
        {
            Array.Clear(charBuffer, 0, charBuffer.Length);
            Array.Clear(colorBuffer, 0, colorBuffer.Length);
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
            cfi.dwFontSize.x = sizeX;
            cfi.dwFontSize.Y = sizeY;
            cfi.FaceName = (sizeX < 4 || sizeY < 4) ? "Consolas" : "Terminal";

            NativeMethods.SetCurrentConsoleFontEx(handle, false, ref cfi);
            return 0;
        }

    }
}