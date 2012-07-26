using System.Windows.Media;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = System.Drawing.Color;

namespace DomFx.Renderers.iTextSharp
{
    public class BackgroundRenderer
    {
        public void Render(PdfWriter writer, FixedElement element)
        {
            var spec = (Backgrounded) element.Specification;

            if (spec.BackgroundColor == Colors.Transparent)
                return;

            var rectangle = new Rectangle((float)element.InnerBox.Left.Points,
                                          writer.PageSize.Height - (float)element.InnerBox.Bottom.Points,
                                          (float)element.InnerBox.Right.Points,
                                          writer.PageSize.Height - (float)element.InnerBox.Top.Points)
            {
                BackgroundColor = new global::iTextSharp.text.Color(Color.FromArgb(spec.BackgroundColor.A,
                                                                                   spec.BackgroundColor.R,
                                                                                   spec.BackgroundColor.G,
                                                                                   spec.BackgroundColor.B))
            };

            writer.DirectContent.Rectangle(rectangle);
        }
    }
}