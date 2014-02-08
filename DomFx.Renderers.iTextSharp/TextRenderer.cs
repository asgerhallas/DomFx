using System.Globalization;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;
using DomFx.Layouters.Specification.Style;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Color = System.Drawing.Color;
using Element = iTextSharp.text.Element;
using Font = iTextSharp.text.Font;

namespace DomFx.Renderers.iTextSharp
{
    public class TextRenderer
    {
        public void Render(PdfWriter writer, FixedElement element)
        {
            var spec = (ITexted) element.Specification;
            var columnText = new ColumnText(writer.DirectContent)
            {
                Leading = (float) ((iTextSharpFont) spec.Font).Leading,
                UseAscender = true
            };
            var font = ((iTextSharpFont)spec.Font).Font;
            font.Color = new global::iTextSharp.text.Color(Color.FromArgb(spec.TextColor.A, spec.TextColor.R, spec.TextColor.G, spec.TextColor.B));
            var text = spec.TextContent.Replace(Text.PageNumber, writer.CurrentPageNumber.ToString(CultureInfo.InvariantCulture));
            columnText.AddText(new Phrase(text, font));

            var horizontalAlignment = 0;
            switch (spec.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    horizontalAlignment = Element.ALIGN_LEFT;
                    break;
                case HorizontalAlignment.Center:
                    horizontalAlignment = Element.ALIGN_CENTER;
                    break;
                case HorizontalAlignment.Right:
                    horizontalAlignment = Element.ALIGN_RIGHT;
                    break;
                case HorizontalAlignment.Justify:
                    horizontalAlignment = Element.ALIGN_JUSTIFIED;
                    break;
            }

            var textHeight = spec.Font.CalculateTextHeight(text, element.InnerBox.Width);
            var top = Unit.Undefined;
            switch (spec.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    top = element.InnerBox.Top;
                    break;
                case VerticalAlignment.Middle:
                    top = element.InnerBox.Top + (element.InnerBox.Height - textHeight)/2;
                    break;
                case VerticalAlignment.Bottom:
                    top = element.InnerBox.Top + element.InnerBox.Height - textHeight;
                    break;
            }

            if (element.VisiblePartOfSpecification.Top > 0.cm())
            {
                columnText.SetSimpleColumn((float) element.InnerBox.Left.Points,
                                           0,
                                           (float) element.InnerBox.Right.Points,
                                           (float) -element.VisiblePartOfSpecification.Top.Points);
                columnText.Go(true);
            }

            if (!string.IsNullOrEmpty(spec.Leader) && element.InnerBox.Height.Points < ((iTextSharpFont) spec.Font).Leading*2)
            {
                var leader = "";
                var textWidth = CalculateTextWidth(text, element, font, (float) ((iTextSharpFont)spec.Font).Leading);
                for (var i = 0; i < 1000; i++)
                    leader += spec.Leader;

                var leaderColumnText = new ColumnText(writer.DirectContent);
                leaderColumnText.UseAscender = true;
                leaderColumnText.SetSimpleColumn(new Phrase(leader, font),
                                                 (float) element.InnerBox.Left.Points + textWidth,
                                                 writer.PageSize.Height - (float) element.InnerBox.Bottom.Points,
                                                 (float) element.InnerBox.Right.Points,
                                                 writer.PageSize.Height - (float) top.Points,
                                                 (float) ((iTextSharpFont) spec.Font).Leading,
                                                 Element.ALIGN_RIGHT);

                leaderColumnText.Go();
            }

            columnText.SetSimpleColumn((float) element.InnerBox.Left.Points,
                                       writer.PageSize.Height - (float) element.InnerBox.Bottom.Points,
                                       (float) element.InnerBox.Right.Points,
                                       writer.PageSize.Height - (float)top.Points,
                                       (float) ((iTextSharpFont) spec.Font).Leading,
                                       horizontalAlignment);
            columnText.Go();
        }

        static float CalculateTextWidth(string text, FixedElement element, Font font, float leading)
        {
            var ct = new ColumnText(null);
            ct.AddText(new Phrase(text, font));

            ct.SetSimpleColumn(0,
                               0,
                               (float) element.InnerBox.Width.Points,
                               (float) element.InnerBox.Height.Points,
                               leading,
                               Element.ALIGN_LEFT);

            ct.Go(true);
            return ct.FilledWidth;
        }
    }
}