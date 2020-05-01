namespace Console3D
{
    public struct Vector3
    {
        #region Properties

        public float x { get; set; }

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

        public Vector3(int x, int y, int z)
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

        #region Public Methods

        public Vector3 With(float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? this.x, y ?? this.y, z ?? this.z);
        }

        public void Select(float? x = null, float? y = null, float? z = null)
        {
            this.x = x ?? this.x;
            this.y = y ?? this.y;
            this.z = z ?? this.z;
        }

        public override string ToString() => $"{x}, {y}, {z}";

        #endregion

        #region Operators

        public static implicit operator Vector3((int x, int y, int z) t)
        {
            return new Vector3(t.x, t.y, t.z);
        }

        public static implicit operator Vector3((float x, float y, float z) t)
        {
            return new Vector3(t.x, t.y, t.z);
        }

        #endregion
    }
}