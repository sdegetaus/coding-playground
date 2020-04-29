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
        #region Properties

        public int width;

        public int height;

        protected PixelArray pixelArray;

        #endregion

        #region Constructors

        public Image(int width, int height)
        {
            this.width = width;
            this.height = height;
            pixelArray = new PixelArray(width, height);
        }

        #endregion

        #region Methods

        public abstract void Save(string savePath);

        public Color GetPixel(int x, int y)
        {
            return pixelArray.GetPixel(y * height + x);
        }

        public void SetPixel(int x, int y, Color color)
        {
            pixelArray.SetPixel(y * height + x, color);
        }

        public void Fill(Color color)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pixelArray.Add(color);
                }
            }
        }

        public void CreateNoise(float amount, bool monochromatic)
        {
            var rand = new System.Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (monochromatic)
                    {
                        var bwColor = Color.white;
                        if (rand.Next(0, 2) != 1)
                        {
                            bwColor = Color.black;
                        }
                        pixelArray.Add(bwColor);
                    }
                    else
                    {
                        pixelArray.Add(Color.random);
                    }
                }
            }
        }

        public void LinearGradient(Color from, Color to)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var perc = (float)y / (float)height; // vertical
                    // var perc = (float)x / (float)height; // horizontal

                    var c = Color.Lerp(from, to, perc);
                    pixelArray.Add(c);
                }
            }
        }

        public void Invert()
        {
            pixelArray.Map((color, index) => color.Invert());
        }

        public void Scale(int newWidth, int newHeight)
        {
            float xScale = (float)newWidth / (float)(width - 1);
            float yScale = (float)newHeight / (float)(height - 1);

            var newPixelArray = new PixelArray(newWidth, newHeight);

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    var color = GetPixel(
                        (int)(1 + x / xScale),
                        (int)(1 + y / yScale)
                    );
                    newPixelArray.Add(color);
                }
            }

            width = newWidth;
            height = newHeight;

            pixelArray = newPixelArray;
        }

        public void Saturate(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void Desaturate()
        {
            pixelArray.Map((color, index) => color.desaturate);
        }

        public void RemoveChannel(ColorChannel channel)
        {
            pixelArray.Map((color, index) =>
            {
                return color.RemoveChannel(channel);
            });
        }

        #endregion

    }
}