using System;

namespace Console3D
{
    public class ConsoleWindow
    {
        #region Properties

        public int Width { get; private set; }
        public int Heigth { get; private set; }

        public string Title
        {
            get => Console.Title;
            set => Console.Title = value;
        }

        #endregion

        #region Properties

        public ConsoleWindow()
        {
            Console.Title = "Default Window";

            Width = 400;
            Heigth = 400;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.BufferWidth = 400;
            Console.BufferHeight = 400;

            // Console.SetWindowSize(400, 400);

            // Console.Clear();
        }

        #endregion

        #region Private Methods
        #endregion
    }
}