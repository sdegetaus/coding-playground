namespace CodingPlayground
{
    public struct Mesh
    {
        #region Properties

        private List<Triangle> triangles;

        public Triangle this[int index]
        {
            get => triangles[index];
            set => triangles[index] = value;
        }

        public int Count
        {
            get => triangles.Count;
        }

        #endregion

        #region Constructors

        public Mesh(int capacity)
        {
            this.triangles = new List<Triangle>(capacity);
        }

        #endregion

        #region Static Properties

        public static Mesh Cube
        {
            get
            {
                var m = new Mesh(12);
                m.triangles.Add(
                    // SOUTH
                    new Triangle((0, 0, 0), (0, 1, 0), (1, 1, 0)),
                    new Triangle((0, 0, 0), (1, 1, 0), (1, 0, 0)),

                    // EAST
                    new Triangle((1, 0, 0), (1, 1, 0), (1, 1, 1)),
                    new Triangle((1, 0, 0), (1, 1, 1), (1, 0, 1)),

                    // NORTH
                    new Triangle((1, 0, 1), (1, 1, 1), (0, 1, 1)),
                    new Triangle((1, 0, 1), (0, 1, 1), (0, 0, 1)),

                    // WEST
                    new Triangle((0, 0, 1), (0, 1, 1), (0, 1, 0)),
                    new Triangle((0, 0, 1), (0, 1, 0), (0, 0, 0)),

                    // TOP
                    new Triangle((0, 1, 0), (0, 1, 1), (1, 1, 1)),
                    new Triangle((0, 1, 0), (1, 1, 1), (1, 1, 0)),

                    // BOTTOM
                    new Triangle((1, 0, 1), (0, 0, 1), (0, 0, 0)),
                    new Triangle((1, 0, 1), (0, 0, 0), (1, 0, 0))
                );
                return m;
            }
        }

        #endregion
    }
}