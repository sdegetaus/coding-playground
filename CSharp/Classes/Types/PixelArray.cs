namespace CodingPlayground
{
    public class PixelArray
    {
        #region Properties

        private List<Color> pixels;

        #endregion

        #region Constructors

        public PixelArray(int width, int height)
        {
            pixels = new List<Color>(width * height);
        }

        public PixelArray(int squared)
        {
            pixels = new List<Color>(squared * squared);
        }

        #endregion

        #region Methods

        public Color this[int index]
        {
            get => pixels[index];
        }

        public void Add(Color color)
        {
            pixels.Add(color);
        }

        public void Clear()
        {
            pixels.Clear();
        }

        public Color GetPixel(int index) => pixels[index];

        public void SetPixel(int index, Color color) => pixels[index] = color;

        public List<Color> Map(Func<Color, int, Color> callback)
        {
            return pixels.Map(callback);
        }

        #endregion
    }
}