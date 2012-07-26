using System.Windows.Media;

namespace DomFx.Layouters.Specification
{
    public interface Backgrounded : ElementSpecification
    {
        Color BackgroundColor { get; set; }
    }
}