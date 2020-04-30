using System.Collections;

namespace CodingPlayground
{
    public class Triangle
    {
        public Vector3[] p;

        public Triangle()
        {
            this.p = new Vector3[3];
        }

        public Triangle(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            this.p = new Vector3[3] { p1, p2, p3 };
        }

        public override string ToString()
        {
            var result = string.Empty;
            for (int i = 0; i < p.Length; i++)
                result += $"T: ({p[i]}) ";
            return result;
        }

        public static implicit operator Triangle(Vector3[] v) => new Triangle(v[0], v[1], v[2]);
    }
}