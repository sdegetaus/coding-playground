namespace CodingPlayground
{
    class Program
    {
        enum Direction
        {
            North,
            NorthEast,
            East,
            SouthEast,
            South,
            SouthWest,
            West,
            NorthWest,
            Test
        }
        static void Main(string[] args)
        {

            var table = new HashTable<Direction, string>(100);
            table.Add(Direction.North, "Norte");
            table.Add(Direction.East, "Este");
            table.Add(Direction.South, "Sur");
            table.Add(Direction.West, "Oeste");

            // table.Debug();

            table.Add(Direction.NorthEast, "Noreste");
            table.Add(Direction.SouthEast, "Sureste");
            table.Add(Direction.SouthWest, "Suroeste");
            table.Add(Direction.NorthWest, "Noroeste");

            table[Direction.NorthWest] = "Hello, world!";
            table[Direction.North] = "Para el norte!";

            table.Debug();

        }

    }
}
