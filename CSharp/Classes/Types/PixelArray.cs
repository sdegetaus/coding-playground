namespace CodingPlayground
{
    public class PixelArray
    {
        private List<Color> pixels;

        public PixelArray(int width, int height)
        {
            pixels = new List<Color>(width * height);
        }

        public Color this[int index]
        {
            get => pixels[index];
        }

        public void Add(Color color)
        {
            pixels.Add(color);
        }

        public List<Color> Map(Func<Color, int, Color> callback)
        {
            return pixels.Map(callback);
        }
    }
}