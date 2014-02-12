using System.Windows.Media;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Layouters.Specification
{
    public interface ITexted : IElement
    {
        IFont Font { get; set; }
        string TextContent { get; set; }
        Color TextColor { get; set; }
        HorizontalAlignment HorizontalAlignment { get; set; }
        VerticalAlignment VerticalAlignment { get; set; }
        string Leader { get; set; }
    }
}