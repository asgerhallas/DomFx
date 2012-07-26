namespace DomFx.Layouters
{
    public class Edge
    {
        public Edge()
        {
        }

        public Edge(Unit top, Unit right, Unit bottom, Unit left)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public Edge(Edge edge)
        {
            Left = edge.Left;
            Right = edge.Right;
            Top = edge.Top;
            Bottom = edge.Bottom;
        }

        public Unit Left { get; set; }
        public Unit Right { get; set; }
        public Unit Top { get; set; }
        public Unit Bottom { get; set; }

        public static Edge None()
        {
            return new Edge();
        }

        public Unit TotalVertical
        {
            get { return Top + Bottom; }
        }

        public Unit TotalHorizontal
        {
            get { return Left + Right; }
        }

        public bool Equals(Edge other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.Left.Equals(Left) && other.Right.Equals(Right) && other.Top.Equals(Top) && other.Bottom.Equals(Bottom);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Edge);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Left.GetHashCode();
                result = (result*397) ^ Right.GetHashCode();
                result = (result*397) ^ Top.GetHashCode();
                result = (result*397) ^ Bottom.GetHashCode();
                return result;
            }
        }
    }
}