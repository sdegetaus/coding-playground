namespace Console3D
{
    using System;
    using System.IO;
    using Microsoft.Win32.SafeHandles;

    class ConsoleBuffer
    {
        private NativeMethods.CharInfo[] CharInfoBuffer { get; set; }
        SafeFileHandle sFileHandle;

        readonly int width, height;

        public ConsoleBuffer(int width, int height)
        {
            this.width = width;
            this.height = height;

            sFileHandle = NativeMethods.CreateFile(
                "CONOUT$",
                0x40000000,
                2,
                IntPtr.Zero,
                FileMode.Open,
                0,
                IntPtr.Zero
            );

            if (!sFileHandle.IsInvalid)
                CharInfoBuffer = new NativeMethods.CharInfo[this.width * this.height];
        }

        public void SetBuffer(char[,] charBuffer, int[,] colorBuffer, int[,] background, ConsoleColor defaultBg)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int i = (y * width) + x;

                    if (charBuffer[x, y] == 0) background[x, y] = (int)defaultBg;

                    CharInfoBuffer[i].Attributes = (short)(colorBuffer[x, y] | (background[x, y] << 4));
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
                new NativeMethods.Coord() { x = (short)width, Y = (short)height },
                new NativeMethods.Coord() { x = 0, Y = 0 }, ref rect
            );
        }
    }
}