namespace CodingPlayground
{
    public struct Matrix4x4
    {
        #region Properties

        private float[,] matrices;

        public float this[int x, int y]
        {
            get
            {
                return matrices[x, y];
            }
            set
            {
                matrices[x, y] = value;
            }
        }

        #endregion

        #region Constructors

        public Matrix4x4(float[,] matrices)
        {
            this.matrices = matrices;
        }

        #endregion

        #region Constructors

        public static Matrix4x4 zero
        {
            get => new Matrix4x4(new float[4, 4]);
        }

        #endregion
    }
}