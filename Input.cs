using System;

namespace Console3D
{
    public static class Input
    {
        public static bool GetKey(ConsoleKey key)
        {
            short s = NativeMethods.GetAsyncKeyState((int)key);
            return (s & 0x8000) > 0 && ConsoleFocused();
        }

        public static bool GetKeyDown(ConsoleKey key)
        {
            int s = Convert.ToInt32(NativeMethods.GetAsyncKeyState((int)key));
            return (s == -32767) && ConsoleFocused();
        }

        private static bool ConsoleFocused()
        {
            return NativeMethods.GetConsoleWindow() == NativeMethods.GetForegroundWindow();
        }

    }
}