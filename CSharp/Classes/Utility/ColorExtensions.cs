namespace CodingPlayground
{
    public static class ColorExtensions
    {
        public static Color With(this Color original, byte? r = null, byte? g = null, byte? b = null)
        {
            return new Color(r ?? original.r, b ?? original.b, g ?? original.g);
        }

        public static byte[] ToBGR(this Color original)
        {
            return new byte[] { original.b, original.g, original.r };
        }
    }
}