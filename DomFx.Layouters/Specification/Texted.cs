using System.Windows.Media;

namespace DomFx.Layouters.Specification
{
    public interface Texted : ElementSpecification
    {
        Font Font { get; set; }
        string TextContent { get; set; }
        Color TextColor { get; set; }
        HorizontalAlignment HorizontalAlignment { get; set; }
        VerticalAlignment VerticalAlignment { get; set; }
        string Leader { get; set; }
    }
}