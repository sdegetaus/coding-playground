namespace CodingPlayground
{
    public struct Color
    {
        public byte r;

        public byte g;

        public byte b;

        public byte a;

        #region Constructors

        public Color(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 0xFF;
        }

        public Color(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Color(int r, int g, int b)
        {
            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
            this.a = 0xFF;

        }

        public Color(int r, int g, int b, int a)
        {
            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
            this.a = (byte)a;
        }

        public Color(byte fill)
        {
            this.r = fill;
            this.g = fill;
            this.b = fill;
            this.a = 0xFF;
        }

        public Color(int fill)
        {
            this.r = (byte)fill;
            this.g = (byte)fill;
            this.b = (byte)fill;
            this.a = 0xFF;
        }

        #endregion

        public Color Invert() => new Color((byte)~r, (byte)~g, (byte)~b);

        public Color RemoveChannel(ColorChannel channel)
        {
            var newColor = this;

            if (channel.HasFlag(ColorChannel.Red))
            {
                newColor = newColor.With(r: 0x00);
            }

            if (channel.HasFlag(ColorChannel.Green))
            {
                newColor = newColor.With(g: 0x00);
            }

            if (channel.HasFlag(ColorChannel.Blue))
            {
                newColor = newColor.With(b: 0x00);
            }

            if (channel.HasFlag(ColorChannel.Alpha))
            {
                newColor = newColor.With(a: 0x00);
            }

            return newColor;
        }

        public Color With(byte? r = null, byte? g = null, byte? b = null, byte? a = null) =>
            new Color(r ?? this.r, g ?? this.g, b ?? this.b, a ?? this.a);

        public Color With(int? r = null, int? g = null, int? b = null, int? a = null) =>
            new Color(r ?? this.r, g ?? this.g, b ?? this.b, a ?? this.a);

        public byte[] ToBGR() => new byte[] { this.b, this.g, this.r };

        public override string ToString() => $"R: {r}, G: {g}, B: {b}";

        #region Operators

        public static Color operator +(Color color1, Color color2)
        {
            var r = color1.r + color2.r > 0xFF ? 255 : color1.r + color2.r;
            var g = color1.g + color2.g > 0xFF ? 255 : color1.g + color2.g;
            var b = color1.b + color2.b > 0xFF ? 255 : color1.b + color2.b;
            return new Color(r, g, b);
        }

        public static Color operator -(Color color1, Color color2)
        {
            var r = color1.r - color2.r < 0x00 ? 0 : color1.r - color2.r;
            var g = color1.g - color2.g < 0x00 ? 0 : color1.g - color2.g;
            var b = color1.b - color2.b < 0x00 ? 0 : color1.b - color2.b;
            return new Color(r, g, b);
        }

        #endregion

        #region Standard Values

        public static Color white
        {
            get => new Color(0xFF, 0xFF, 0xFF);
        }

        public static Color black
        {
            get => new Color(0x00, 0x00, 0x00);
        }

        public static Color red
        {
            get => new Color(0xFF, 0x00, 0x00);
        }

        public static Color green
        {
            get => new Color(0x00, 0xFF, 0x00);
        }

        public static Color blue
        {
            get => new Color(0x00, 0x00, 0xFF);
        }

        public static Color yellow
        {
            get => new Color(0xFF, 0xFF, 0x00);
        }

        public static Color magenta
        {
            get => new Color(0xFF, 0x00, 0xFF);
        }

        public static Color cyan
        {
            get => new Color(0x00, 0xFF, 0xFF);
        }

        public static Color random
        {
            get
            {
                var rand = new System.Random();
                var color = new byte[] { 0x00, 0x00, 0x00 };
                rand.NextBytes(color);
                return new Color(color[0], color[1], color[2]);
            }
        }

        #endregion

    }
}