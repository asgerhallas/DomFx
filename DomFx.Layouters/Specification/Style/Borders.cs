using System.Windows.Media;

namespace DomFx.Layouters.Specification.Style
{
    public class Borders : Edge
    {
        public Borders()
        {
        }

        public Borders(Unit top, Unit right, Unit bottom, Unit left, Color color) : base(top, right, bottom, left)
        {
            Color = color;
        }

        public Borders(Borders borders) : base(borders)
        {
            Color = borders.Color;
        }

        public Color Color { get; private set; }
    }
}