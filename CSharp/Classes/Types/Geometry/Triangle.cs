namespace CodingPlayground
{
    public struct Triangle
    {
        #region Properties

        private Vector3 p0;

        private Vector3 p1;

        private Vector3 p2;

        public Vector3 this[int index]
        {
            get
            {
                switch (index)
                {
                    default:
                        throw new System.NullReferenceException();
                    case 0:
                        return p0;
                    case 1:
                        return p1;
                    case 2:
                        return p2;
                }
            }
            set
            {
                switch (index)
                {
                    default:
                        throw new System.NullReferenceException();
                    case 0:
                        p0 = value;
                        break;
                    case 1:
                        p1 = value;
                        break;
                    case 2:
                        p2 = value;
                        break;
                }
            }
        }

        #endregion

        #region Constructors

        public Triangle(Vector3 p0, Vector3 p1, Vector3 p2)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
        }

        #endregion

        #region Public Methods

        public static Triangle zero
        {
            get => new Triangle((0, 0, 0), (0, 0, 0), (0, 0, 0));
        }

        #endregion

        #region Public Methods

        public override string ToString() => $"{p0}, {p1}, {p2}";

        #endregion
    }
}