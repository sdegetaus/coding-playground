namespace CodingPlayground
{
    [System.Flags]
    public enum ColorChannel
    {
        Red = 1,
        Blue = 2,
        Green = 4,
        Alpha = 8,
    }

    public abstract class Image
    {
        public int width;

        public int height;

        protected PixelArray pixelArray;

        public Image(int width, int height)
        {
            this.width = width;
            this.height = height;
            pixelArray = new PixelArray(width, height);
        }

        public abstract void Save(string savePath);

        public void Invert()
        {
            pixelArray.Map((color, index) => color.Invert());
        }

        public void Saturate(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void Desaturate()
        {
            pixelArray.Map((color, index) =>
            {
                return new Color((byte)(color.r + color.b + color.g) / 0x03);
            });
        }

        public void RemoveChannel(ColorChannel channel)
        {
            pixelArray.Map((color, index) =>
            {
                return color.RemoveChannel(channel);
            });
        }

    }
}