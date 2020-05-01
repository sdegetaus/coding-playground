using System;
using System.IO;

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

        #region 

        public bool LoadFromFile(string filename)
        {
            using (var file = new StreamReader(filename))
            {
                triangles = new List<Triangle>();
                var vertices = new List<Vector3>();
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    // skip empty lines
                    if (line.Length == 0) continue;

                    if (line[0] == 'v')
                    {
                        var points = line.Split(' ');
                        vertices.Add(
                            new Vector3(
                                float.Parse(points[1]),
                                float.Parse(points[2]),
                                float.Parse(points[3])
                            )
                        );
                    }

                    if (line[0] == 'f')
                    {
                        var f = line.Split(' ');
                        int[] faces = {
                            Convert.ToInt32(f[1]),
                            Convert.ToInt32(f[2]),
                            Convert.ToInt32(f[3])
                        };

                        triangles.Add(
                            new Triangle(vertices[faces[0] - 1], vertices[faces[1] - 1], vertices[faces[2] - 1])
                        );
                    }
                }
            }
            return true;
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