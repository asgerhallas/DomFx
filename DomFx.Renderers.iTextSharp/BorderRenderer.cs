using DomFx.Layouters;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DomFx.Renderers.iTextSharp
{
    public class BorderRenderer
    {
        public void Render(PdfWriter writer, FixedElement element)
        {
            var spec = element.Specification;

            var rectangle = new Rectangle((float) element.BorderBox.Left.Points,
                                          writer.PageSize.Height - (float)element.BorderBox.Bottom.Points,
                                          (float)element.BorderBox.Right.Points,
                                          writer.PageSize.Height - (float)element.BorderBox.Top.Points)
            {
                BorderWidthTop = (float) spec.Borders.Top.Points,
                BorderWidthRight = (float) spec.Borders.Right.Points,
                BorderWidthBottom = (float) spec.Borders.Bottom.Points,
                BorderWidthLeft = (float) spec.Borders.Left.Points,
                BorderColor = spec.Borders.Color.ToiTextSharpColor()
            };

            writer.DirectContent.Rectangle(rectangle);
        }
    }
}