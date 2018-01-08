namespace Triangles
{

    public struct Coordinates
    {
        public int Y { get; set; }
        public int X { get; set; }

        public override string ToString()
        {
            return $"{Y}, {X}";
        }
    }
}
