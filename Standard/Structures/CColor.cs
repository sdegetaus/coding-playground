using System;

namespace ConsoleGraphics
{
    public struct CColor
    {
        public ConsoleColor fg;
        public ConsoleColor bg;
        public PixelSymbol sym;

        public CColor(
            ConsoleColor fg, ConsoleColor bg, PixelSymbol sym)
        {
            this.fg = fg;
            this.bg = bg;
            this.sym = sym;
        }

        public static CColor empty
        {
            get => new CColor(
                ConsoleColor.Black,
                ConsoleColor.Black,
                PixelSymbol.NULL
            );
        }

    }
}