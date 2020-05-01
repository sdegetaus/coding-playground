using System.Text;
using Console3D.Collections;

namespace Console3D
{
    public class Bitmap : Image
    {
        #region Properties

        private List<byte> headerData = new List<byte>();

        #endregion

        #region Constructors

        public Bitmap(int width, int height) : base(width, height) { }

        #endregion

        #region Methods

        private void FillHeader()
        {
            headerData.Clear();
            headerData.Add(Encoding.ASCII.GetBytes("BM"));              // 00
            headerData.Add(new byte[] { 0x46, 0x00, 0x00, 0x00 });      // 02
            headerData.Add(new byte[] { 0x00, 0x00 });                  // 06
            headerData.Add(new byte[] { 0x00, 0x00 });                  // 08
            headerData.Add(new byte[] { 0x36, 0x00, 0x00, 0x00 });      // 0A
            headerData.Add(new byte[] { 0x28, 0x00, 0x00, 0x00 });      // 0E
            headerData.Add(System.BitConverter.GetBytes(width));        // 12
            headerData.Add(System.BitConverter.GetBytes(height));       // 16
            headerData.Add(new byte[] { 0x01, 0x00 });                  // 1A
            headerData.Add(new byte[] { 0x18, 0x00 });                  // 1C
            headerData.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });      // 1E
            headerData.Add(new byte[] { 0x10, 0x00, 0x00, 0x00 });      // 22
            headerData.Add(new byte[] { 0x13, 0x0B, 0x00, 0x00 });      // 26
            headerData.Add(new byte[] { 0x13, 0x0B, 0x00, 0x00 });      // 2A
            headerData.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });      // 2E
            headerData.Add(new byte[] { 0x00, 0x00, 0x00, 0x00 });      // 32
        }

        public override void Save(string savePath)
        {
            if (pixelArray.size == 0)
            {
                throw new System.NullReferenceException("The bitmap you are trying to save is empty.");
            }

            if (!System.IO.File.Exists(savePath))
            {
                System.IO.File.Create(savePath);
            }
            else
            {
                System.IO.File.Delete(savePath);
            }

            using (var s = System.IO.File.OpenWrite(savePath))
            {
                var bw = new System.IO.BinaryWriter(s);

                FillHeader();
                headerData.ForEach(bytes => bw.Write(bytes));

                var padding = new byte[width % 4];
                var counter = 0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
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

        public void New()
        {
            headerData.Clear();
            pixelArray.Clear();
            pixelArray = new PixelArray(width, height);
        }

        #endregion

    }
}