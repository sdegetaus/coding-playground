namespace CodingPlayground
{
    public struct Mesh
    {
        public List<Triangle> tris;

        public Mesh(int capacity)
        {
            tris = new List<Triangle>(capacity);
        }

    }
}