namespace CodingPlayground
{
    public struct Color
    {
        public byte r;

        public byte g;

        public byte b;

        public Color(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public Color(int r, int g, int b)
        {
            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
        }

        public override string ToString()
        {
            return $"R: {r}, G: {g}, B: {b}";
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

        public static Color blue
        {
            get => new Color(0x00, 0xFF, 0x00);
        }

        public static Color green
        {
            get => new Color(0x00, 0x00, 0xFF);
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
    }
}