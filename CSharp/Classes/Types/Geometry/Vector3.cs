namespace CodingPlayground
{
    public struct Vector3
    {
        #region Properties

        public float x;

        public float y;

        public float z;

        #endregion

        #region Constructors

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #endregion

        #region Static Properties

        /// <summary>
        /// Shorthand for: 0, 0, -1
        /// </summary>
        public static Vector3 back
        {
            get => new Vector3(0, 0, -1);
        }

        /// <summary>
        /// Shorthand for: 0, -1, 0
        /// </summary>
        public static Vector3 down
        {
            get => new Vector3(0, -1, 0);
        }

        /// <summary>
        /// Shorthand for: 0, 0, 1
        /// </summary>
        public static Vector3 forward
        {
            get => new Vector3(0, 0, 1);
        }

        /// <summary>
        /// Shorthand for: -1, 0, 0
        /// </summary>
        public static Vector3 left
        {
            get => new Vector3(-1, 0, 0);
        }

        /// <summary>
        /// Shorthand for: 1, 1, 1
        /// </summary>
        public static Vector3 one
        {
            get => new Vector3(1, 1, 1);
        }

        /// <summary>
        /// Shorthand for: 1, 0, 0
        /// </summary>
        public static Vector3 right
        {
            get => new Vector3(1, 0, 0);
        }

        /// <summary>
        /// Shorthand for: 0, 1, 0
        /// </summary>
        public static Vector3 up
        {
            get => new Vector3(0, 1, 0);
        }

        /// <summary>
        /// Shorthand for: 0, 0, 0
        /// </summary>
        public static Vector3 zero
        {
            get => new Vector3(0, 0, 0);
        }

        #endregion

        public override string ToString()
        {
            return $"{x}, {y}, {z}";
        }

        public static implicit operator Vector3((int x, int y, int z) t) => new Vector3(t.x, t.y, t.z);
        public static implicit operator Vector3((float x, float y, float z) t) => new Vector3(t.x, t.y, t.z);

    }
}