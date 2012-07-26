using DomFx.Layouters;
using PdfSharp.Drawing;

namespace DomFx.Renderers.PdfSharp.WPF
{
    public class BorderRenderer
    {
        public void Render(XGraphics gfx, FixedElement element)
        {
            var spec = element.Specification;

            var penTop = new XPen(XColor.FromArgb(spec.Borders.Color), spec.Borders.Top.Points);
            var penLeft = new XPen(XColor.FromArgb(spec.Borders.Color), spec.Borders.Left.Points);
            var penRight = new XPen(XColor.FromArgb(spec.Borders.Color), spec.Borders.Right.Points);
            var penBottom = new XPen(XColor.FromArgb(spec.Borders.Color), spec.Borders.Bottom.Points);

            var midBorderTop = element.InnerBox.Top - spec.Borders.Top / 2;
            var midBorderRight = element.InnerBox.Right + spec.Borders.Right / 2;
            var midBorderBottom = element.InnerBox.Bottom + spec.Borders.Bottom / 2;
            var midBorderLeft = element.InnerBox.Left - spec.Borders.Left / 2;

            if (spec.Borders.Top != Unit.Zero)
                gfx.DrawLine(penTop, element.InnerBox.Left.Points, midBorderTop.Points, element.InnerBox.Right.Points, midBorderTop.Points);

            if (spec.Borders.Right != Unit.Zero)
                gfx.DrawLine(penRight, midBorderRight.Points, element.MarginBox.Top.Points, midBorderRight.Points, element.MarginBox.Bottom.Points);

            if (spec.Borders.Bottom != Unit.Zero)
                gfx.DrawLine(penBottom, element.InnerBox.Left.Points, midBorderBottom.Points, element.InnerBox.Right.Points, midBorderBottom.Points);

            if (spec.Borders.Left != Unit.Zero)
                gfx.DrawLine(penLeft, midBorderLeft.Points, element.MarginBox.Top.Points, midBorderLeft.Points, element.MarginBox.Bottom.Points);
        }
    }
}