namespace ConsoleGraphics
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

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    pixelArray.Add(Color.white);
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

        public void SetPixel(Vector2 pos, Color color)
        {
            pixelArray.SetPixel(pos.y * height + pos.x, color);
        }

        public void Fill(int x, int y, int w, int h, Color color)
        {
            for (int _y = 0; _y < h; _y++)
            {
                for (int _x = 0; _x < w; _x++)
                {
                    Vector2 pos = new Vector2(x + _x, y + _y);

                    if (pos.x < 0 ||
                        pos.y < 0 ||
                        pos.x >= width ||
                        pos.y >= width)
                    {
                        continue;
                    }

                    // var perc = (float)pos.x / (float)w;
                    // var c = Color.Lerp(gradient.from, gradient.to, perc);
                    SetPixel(pos, color);
                }
            }
        }

        public void DrawLine(Vector2 pos1, Vector2 pos2, Color color)
        {
            var f = Vector2.Distance(pos1, pos2);

            for (int i = 0; i < f; i++)
            {
                var lerp = Vector2.Lerp(pos1, pos2, (1f / f) * i);
                var finalPos = lerp;

                if (finalPos.x < 0 ||
                    finalPos.y < 0 ||
                    finalPos.x >= width ||
                    finalPos.y >= width)
                {
                    continue;
                }

                // SetPixel(finalPos.x, finalPos.y, color);
            }
        }

        public void DrawTriangle(int x1, int y1, int x2, int y2, int x3, int y3, Color c)
        {
            DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), c);
            DrawLine(new Vector2(x2, y2), new Vector2(x3, y3), c);
            DrawLine(new Vector2(x3, y3), new Vector2(x1, y1), c);
        }

        public void FillTriangle(int x0, int y0, int x1, int y1, int x2, int y2, Color c)
        {
            // TODO: improve (no width or height)
            float area = 0.5f * (-y1 * x2 + y0 * (-x1 + x2) + x0 * (y1 - y2) + x1 * y2);

            for (int _y = 0; _y < height; _y++)
            {
                for (int _x = 0; _x < width; _x++)
                {
                    Vector2 pos = new Vector2(_x, _y);

                    if (pos.x < 0 ||
                        pos.y < 0 ||
                        pos.x >= width ||
                        pos.y >= width)
                    {
                        continue;
                    }

                    float s = 1f / (2f * area) * (y0 * x2 - x0 * y2 + (y2 - y0) * pos.x + (x0 - x2) * pos.y);
                    float t = 1f / (2f * area) * (x0 * y1 - y0 * x1 + (y0 - y1) * pos.x + (x1 - x0) * pos.y);

                    if (s > 0f && t > 0f && 1f - s - t > 0f)
                    {
                        SetPixel(pos, c);
                    }
                    else { continue; }
                }
            }
        }

        public void DrawCircle(int x, int y, int r, Color color)
        {
            for (int _y = 0; _y <= r * 2; _y++)
            {
                for (int _x = 0; _x <= r * 2; _x++)
                {
                    var posX = x + _x;
                    var posY = y + _y;
                    if (posX < 0) posX = 0;
                    if (posX >= width) posX = width - 1;
                    if (posY < 0) posY = 0;
                    if (posY >= height) posY = height - 1;

                    if ((_x - r) * (_x - r) + (_y - r) * (_y - r) <= r * r)
                    {
                        SetPixel(posX, posY, color);
                    }
                }
            }
        }

        #endregion

        #region Effects

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

        public void LinearGradient(Color from, Color to)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var perc = (float)y / (float)height;    // vertical

                    // var perc = (float)x / (float)height; // horizontal

                    // var perc = (float)-(x + 1f) / (float)(width) *
                    //            (float)(y + 1f) / (float)(height); // horizontal

                    var c = Color.Lerp(from, to, perc);
                    pixelArray.Add(c);
                }
            }
        }

        public void RadialGradient(Color from, Color to)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var mX = width / 2f;
                    var mY = height / 2f;

                    var perc = 2f * (float)((x - mX) * (x - mX) + (y - mY) * (y - mY)) /
                                    (float)(width * height);

                    var c = Color.Lerp(from, to, perc);
                    pixelArray.Add(c);
                }
            }
        }

        #endregion
    }
}