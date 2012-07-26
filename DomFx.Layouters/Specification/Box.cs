using System.Windows.Media;

namespace DomFx.Layouters.Specification
{
    public class Box : ElementSpecificationAbstract, Backgrounded
    {
        public Box()
        {
            BackgroundColor = Colors.Transparent;
        }

        public Color BackgroundColor { get; set; }
    }
}