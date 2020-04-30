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

        public void Noise()
        {
            var rand = new System.Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var c = new Color((int)((rand.NextDouble() * 255f)));
                    pixelArray.Add(c);
                }
            }
        }

        public void PerlinNoise(int octaves, float bias)
        {
            var rand = new System.Random();
            float[] seeds = new float[width * height];
            for (int i = 0; i < seeds.Length; i++) seeds[i] = (float)rand.NextDouble();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float noise = 0f;
                    float scale = 1f;
                    float scaleAcc = 0f;

                    for (int o = 0; o < octaves; o++)
                    {
                        int pitch = width >> o;
                        int sampleX1 = (x / pitch) * pitch;
                        int sampleY1 = (y / pitch) * pitch;

                        int sampleX2 = (sampleX1 + pitch) % width;
                        int sampleY2 = (sampleY1 + pitch) % height;

                        float blendX = (float)(x - sampleX1) / (float)pitch;
                        float blendY = (float)(y - sampleY1) / (float)pitch;

                        float sampleT = (1.0f - blendX) * seeds[sampleY1 * width + sampleX1] + blendX * seeds[sampleY1 * width + sampleX2];
                        float sampleB = (1.0f - blendX) * seeds[sampleY2 * width + sampleX1] + blendX * seeds[sampleY2 * width + sampleX2];

                        noise += (blendY * (sampleB - sampleT) + sampleT) * scale;

                        scaleAcc += scale;
                        scale = scale / bias;
                    }
                    var c = new Color((int)((noise / scaleAcc) * 255f));
                    pixelArray.Add(c);
                }
            }
        }

        public void LinearGradient(Color from, Color to)
        {
            float yoff = 0f;
            float xoff = 0f;
            for (int y = 0; y < height; y++)
            {
                xoff = 0;
                for (int x = 0; x < width; x++)
                {
                    var perc = (float)y / (float)height; // vertical
                    // var perc = (float)x / (float)height; // horizontal
                    // var perc = (float)-(x + 1f) / (float)(width) *
                    //            (float)(y + 1f) / (float)(height); // horizontal
                    // System.Console.WriteLine(perc);
                    // float perc = 0.5f;

                    var c = Color.Lerp(from, to, perc);
                    pixelArray.Add(c);
                    xoff += -.5f;
                }
                yoff += -.5f;
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

        // experimental
        public void DrawRectangle(int posX, int posY, int _width, int _height, Color color)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    System.Console.WriteLine(posX + x);
                    SetPixel(posX + x, posY + y, color);
                }
            }
        }

        public void DrawCircle(int posX, int  posY, int r, Color color)
        {
            for (int x = posX - r; x <= posX + r * 2; x++)
            {
                for (int y = posY - r; y <= posY + r * 2; y++)
                {
                    var f = (x - r) * (x - r) + (y - r) * (y - r);
                    System.Console.WriteLine(f);
                    if (f <= r * r)
                    {
                        SetPixel(x, y, color);
                    }
                }
            }
        }


        // for(int i = start_X - r; i <= start_X + r; i++)
        // {
        //    for(int j = start_Y - r; j <= start_Y + r; j++)
        //    {
        //        if((i-r)*(i-r) + (j-r)*(j-r) <= r*r)
        //        {
        //          circlePoints.push_back(std::pair<int>(i,j));
        //        }
        //    }
        // }

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