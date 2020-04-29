namespace CodingPlayground
{
    public struct Color
    {
        #region Properties

        public byte r;

        public byte g;

        public byte b;

        public byte a;

        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return r;
                    case 1: return g;
                    case 2: return b;
                    case 3: return a;
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        r = value;
                        break;
                    case 1:
                        g = value;
                        break;
                    case 2:
                        b = value;
                        break;
                    case 3:
                        a = value;
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

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

        #region Methods

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

        #endregion

        #region Static Methods

        public static Color Lerp(Color start, Color end, float percent)
        {
            if (percent == 0f) return start;
            if (percent == 1f) return end;
            return start + percent * (end - start);
        }

        #endregion

        #region Operators

        private static int Sanitize(int value)
        {
            if (value > 0xFF) return 0xFF;
            if (value < 0x00) return 0x00;
            return value;
        }

        public static Color operator +(Color color1, Color color2)
        {
            var r = Sanitize(color1.r + color2.r);
            var g = Sanitize(color1.g + color2.g);
            var b = Sanitize(color1.b + color2.b);
            return new Color(r, g, b);
        }

        public static Color operator -(Color color1, Color color2)
        {
            var r = Sanitize(color1.r - color2.r);
            var g = Sanitize(color1.g - color2.g);
            var b = Sanitize(color1.b - color2.b);
            return new Color(r, g, b);
        }

        public static Color operator *(Color color, float factor) => factor * color;

        public static Color operator *(float factor, Color color)
        {
            var r = Sanitize((int)(factor * color.r));
            var g = Sanitize((int)(factor * color.g));
            var b = Sanitize((int)(factor * color.b));
            return new Color(r, g, b);
        }

        #endregion

        #region Static Properties

        public Color desaturate
        {
            get => new Color((byte)(r + b + g) / 0x03);
        }

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