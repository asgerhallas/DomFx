using System.Windows.Media;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Specification
{
    public interface IBackgrounded : IElement
    {
        Color BackgroundColor { get; set; }
    }
}