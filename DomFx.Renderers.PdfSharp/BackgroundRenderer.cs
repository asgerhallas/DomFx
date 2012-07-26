using System.Windows.Media;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using PdfSharp.Drawing;

namespace DomFx.Renderers.PdfSharp.WPF
{
    public class BackgroundRenderer
    {
        public void Render(XGraphics gfx, FixedElement element)
        {
            var spec = (Backgrounded) element.Specification;

            if (spec.BackgroundColor != Colors.Transparent)
            {
                var backgroundBrush = new XSolidBrush(XColor.FromArgb(spec.BackgroundColor));
                gfx.DrawRectangle(backgroundBrush,
                                  element.InnerBox.Left.Points,
                                  element.InnerBox.Top.Points,
                                  element.InnerBox.Width.Points,
                                  element.InnerBox.Height.Points);
            }
        }
    }
}