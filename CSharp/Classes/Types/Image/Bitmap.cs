using System;
using System.Text;

namespace CodingPlayground
{
    public class Bitmap : Image
    {
        private List<byte> headerData = new List<byte>();

        public Bitmap(int width, int height) : base(width, height)
        {
            FillHeader();
        }

        private void FillHeader()
        {
            headerData.Add(Encoding.ASCII.GetBytes("BM"));              // 00
            headerData.Add(new byte[] { 0x46, 0x00, 0x00, 0x00 });      // 02
            headerData.Add(new byte[] { 0x00, 0x00 });                  // 06
            headerData.Add(new byte[] { 0x00, 0x00 });                  // 08
            headerData.Add(new byte[] { 0x36, 0x00, 0x00, 0x00 });      // 0A
            headerData.Add(new byte[] { 0x28, 0x00, 0x00, 0x00 });      // 0E
            headerData.Add(BitConverter.GetBytes(width));               // 12
            headerData.Add(BitConverter.GetBytes(height));              // 16
            headerData.Add(new byte[] { 0x01, 0x00 });                  // 1A
            headerData.Add(new byte[] { 0x18, 0x00 });                  // 1C
            headerData.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });      // 1E
            headerData.Add(new byte[] { 0x10, 0x00, 0x00, 0x00 });      // 22
            headerData.Add(new byte[] { 0x13, 0x0B, 0x00, 0x00 });      // 26
            headerData.Add(new byte[] { 0x13, 0x0B, 0x00, 0x00 });      // 2A
            headerData.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });      // 2E
            headerData.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });      // 32
        }

        public void FillContent(bool random)
        {
            var alternate = true;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (random)
                    {
                        pixelArray.Add(Color.random);
                    }
                    else
                    {
                        pixelArray.Add(alternate ? Color.black : Color.white);
                        alternate = !alternate;
                    }
                }

                if (!random && width % 2 == 0)
                {
                    alternate = !alternate;
                }
            }
        }

        public override void Save(string savePath)
        {
            if (!System.IO.File.Exists(savePath))
                System.IO.File.Create(savePath);
            else
                System.IO.File.Delete(savePath);

            using (var s = System.IO.File.OpenWrite(savePath))
            {
                var bw = new System.IO.BinaryWriter(s);
                headerData.ForEach(bytes => bw.Write(bytes));

                var padding = new byte[width % 4];
                var counter = 0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        System.Console.WriteLine(string.Join(", ", pixelArray[counter].ToBGR()));
                        bw.Write(pixelArray[counter].ToBGR());
                        counter++;
                    }

                    for (int i = 0; i < padding.Length; i++)
                    {
                        padding[i] = 0x00;
                    }

                    bw.Write(padding);

                }
            }

        }

    }
}