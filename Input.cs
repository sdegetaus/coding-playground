using System;

namespace Console3D
{
    public static class Input
    {
        // TODO: unfinished, not working properly.
        // for more info: https://www.cplusplus.com/forum/windows/6632/#msg30578

        private static short GetKeyState(ConsoleKey key)
        {
            return Native.GetKeyState((int)key);
        }

        public static bool GetKeyDown(ConsoleKey key)
        {
            short s = Native.GetAsyncKeyState((int)key);
            return s == -32767 && s != -32768 && ConsoleWindow.IsFocused();
        }

        public static bool GetKey(ConsoleKey key)
        {
            short s = Native.GetAsyncKeyState((int)key);
            return (s & 0x8000) > 0 && ConsoleWindow.IsFocused();
        }

        public static bool GetKeyUp(ConsoleKey key)
        {
            throw new NotImplementedException();
        }


        public static bool DebugKeys(ConsoleKey key)
        {
            // int s = Native.GetKeyState((int)key);
            // Debug.Log(s);
            return false;
            // return (s & 0x8000) > 0 && ConsoleWindow.IsFocused();
        }



    }
}