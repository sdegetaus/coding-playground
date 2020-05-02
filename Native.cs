// Base code taken from: 
// https://github.com/ollelogdahl/ConsoleGameEngine

using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Console3D
{
    internal class Native
    {
        public static readonly IntPtr stdOutputHandle = GetStdHandle(-11);
        public static readonly IntPtr stdInputHandle = GetStdHandle(-10);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern short GetAsyncKeyState(Int32 vKey);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern short GetKeyState(Int32 vKey);

        // [DllImport("kernel32.dll", SetLastError = true)]
        // public static extern short ReadConsoleInput(
        // // IntPtr hConsoleInput,
        // // INPUT_RECORD lpBuffer,
        // // int nLength,
        // // int lpNumberOfEventsRead
        // );


        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetCursorPos(out POINT vKey);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(
            string fileName,

            [MarshalAs(UnmanagedType.U4)]
            uint fileAccess,

            [MarshalAs(UnmanagedType.U4)]
            uint fileShare,

            IntPtr securityAttributes,

            [MarshalAs(UnmanagedType.U4)]
            FileMode creationDisposition,

            [MarshalAs(UnmanagedType.U4)]
            int flags,

            IntPtr template
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutputW(
            SafeFileHandle hConsoleOutput,
            CHAR_INFO[] lpBuffer,
            COORD dwBufferSize,
            COORD dwBufferCoord,
            ref SMALL_RECT lpWriteRegion
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 SetCurrentConsoleFontEx(
            IntPtr ConsoleOutput,
            bool MaximumWindow,
            ref CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 GetTickCount();

        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
            public COORD(short x, short y)
            {
                this.X = x;
                this.Y = y;
            }
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        // [StructLayout(LayoutKind.Explicit)]
        // public struct INPUT_RECORD
        // {
        //     [FieldOffset(0)]
        //     public short EventType;
        // }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct CHAR_INFO
        {
            [FieldOffset(0)]
            public char UnicodeChar;

            [FieldOffset(0)]
            public byte AsciiChar;

            [FieldOffset(2)]
            public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CONSOLE_FONT_INFO_EX
        {
            public uint cbSize;

            public uint nFont;

            public COORD dwFontSize;

            public int FontFamily;

            public int FontWeight;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }



        #endregion
    }
}