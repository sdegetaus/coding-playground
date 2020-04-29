using System;
using System.IO;
using System.Text;

namespace CodingPlayground.ImageProcessing
{
    public class Bitmap
    {
        public int width;

        public int height;

        private List<byte> headerData = new List<byte>();

        private List<byte> pixelData = new List<byte>();

        // Private Variables
        // private int sizeHeader;

        // private int bitDepth = 24;

        public Bitmap(int width, int height)
        {
            this.width = width;
            this.height = height;
            FillHeader();
        }

        private void FillHeader()
        {
            // 00
            headerData.Add(Encoding.ASCII.GetBytes("BM"));
            // 02
            headerData.Add(new byte[] { 0x46, 0x00, 0x00, 0x00 });
            // 06
            headerData.Add(new byte[] { 0x00, 0x00 });
            // 08
            headerData.Add(new byte[] { 0x00, 0x00 });
            // 0A
            headerData.Add(new byte[] { 0x36, 0x00, 0x00, 0x00 });
            // 0E
            headerData.Add(new byte[] { 0x28, 0x00, 0x00, 0x00 });
            // 12
            headerData.Add(BitConverter.GetBytes(width));
            // 16
            headerData.Add(BitConverter.GetBytes(height));
            // 1A
            headerData.Add(new byte[] { 0x01, 0x00 });
            // 1C
            headerData.Add(new byte[] { 0x18, 0x00 });
            // 1E
            headerData.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            // 22
            headerData.Add(new byte[] { 0x10, 0x00, 0x00, 0x00 });
            // 26
            headerData.Add(new byte[] { 0x13, 0x0B, 0x00, 0x00 });
            // 2A
            headerData.Add(new byte[] { 0x13, 0x0B, 0x00, 0x00 });
            // 2E
            headerData.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            // 32
            headerData.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            // headerData.ForEach(x => System.Console.WriteLine(x));
        }

        public void FillContent(bool random)
        {
            byte[] whitePixel = { 0xFF, 0xFF, 0xFF };
            byte[] blackPixel = { 0x00, 0x00, 0x00 };

            var padding = new byte[width % 4];
            var alternate = true;

            Random rand = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (random)
                    {
                        var randomPixel = new byte[] { 0x00, 0x00, 0x00 };
                        rand.NextBytes(randomPixel);
                        pixelData.Add(randomPixel);
                    }
                    else
                    {
                        pixelData.Add(alternate ? blackPixel : whitePixel);
                        alternate = !alternate;
                    }
                }

                if (!random && width % 2 == 0)
                    alternate = !alternate;

                for (int i = 0; i < padding.Length; i++)
                    padding[i] = 0x00;

                pixelData.Add(padding);
            }
        }

        public void Invert()
        {
            pixelData = pixelData.Map((val, index) => (byte)~val);
        }

        public void Save(string savePath)
        {
            if (!File.Exists(savePath))
                File.Create(savePath);
            else
                File.Delete(savePath);

            using (var s = File.OpenWrite(savePath))
            {
                var bw = new BinaryWriter(s);
                headerData.ForEach(bytes => bw.Write(bytes));
                pixelData.ForEach(bytes => bw.Write(bytes));
            }

        }

    }
}