namespace ConsoleGraphics
{
    public struct Vector2
    {
        public int x;

        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString() => $"{x}, {y}";

        public static Vector2 zero
        {
            get => new Vector2(0, 0);
        }

        public static float Distance(Vector2 pos1, Vector2 pos2)
        {
            // TODO: implement custom Math.Sqrt
            return (float)System.Math.Sqrt(
                (pos2.x - pos1.x) * (pos2.x - pos1.x) +
                (pos2.y - pos1.y) * (pos2.y - pos1.y)
            );
        }

        public static Vector2 Lerp(Vector2 start, Vector2 end, float percent)
        {
            if (percent == 0f) return start;
            if (percent == 1f) return end;
            return start + percent * (end - start);
        }

        public static Vector2 operator +(Vector2 pos1, Vector2 pos2)
        {
            int x = (int)(pos1.x + pos2.x);
            int y = (int)(pos1.y + pos2.y);
            return new Vector2(x, y);
        }

        public static Vector2 operator -(Vector2 pos1, Vector2 pos2)
        {
            int x = (int)(pos1.x - pos2.x);
            int y = (int)(pos1.y - pos2.y);
            return new Vector2(x, y);
        }

        public static Vector2 operator *(Vector2 color, float factor) => factor * color;

        public static Vector2 operator *(float factor, Vector2 pos)
        {
            int x = (int)(pos.x * factor);
            int y = (int)(pos.y * factor);
            return new Vector2(x, y);
        }

        public static implicit operator Vector2(Vector3 v3) => new Vector2((int)v3.x, (int)v3.y);
    }
}