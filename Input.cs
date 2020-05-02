using System;

namespace Console3D
{
    public static class Input
    {
        // TODO: unfinished, not working properly.
        // for more info: https://www.cplusplus.com/forum/windows/6632/#msg30578

        public static bool GetKeyDown(ConsoleKey key)
        {
            short s = Native.GetAsyncKeyState((int)key);
            bool result = s == -32767 && s != -32768 && ConsoleWindow.IsFocused();
            return result;
        }

        public static bool GetKey(ConsoleKey key)
        {
            short s = Native.GetAsyncKeyState((int)key);
            bool result = (s & 0x8000) > 0 && ConsoleWindow.IsFocused();
            return result;
        }

        public static bool GetKeyUp(ConsoleKey key)
        {
            throw new NotImplementedException();
        }

    }
}