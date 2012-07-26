using DomFx.Layouters;
using DomFx.Layouters.Specification;
using PdfSharp.Drawing;

namespace DomFx.Renderers.PdfSharp.WPF
{
    public class ImageRenderer
    {
        public void Render(XGraphics gfx, FixedElement element)
        {
            var specification = (Image) element.Specification;
            var source = ((PdfSharpImage) specification.Source).Source;

            gfx.DrawImage(source,
                          element.InnerBox.Left.Points,
                          element.InnerBox.Top.Points - element.VisiblePartOfSpecification.Top.Points,
                          element.InnerBox.Width.Points,
                          source.PixelHeight);
        }
    }
}