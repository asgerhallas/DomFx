using DomFx.Layouters;
using DomFx.Layouters.Specification;
using PdfSharp.Drawing;

namespace DomFx.Renderers.PdfSharp.WPF
{
    public class TextRenderer
    {
        public void Render(XGraphics gfx, FixedElement element)
        {
            var specification = (Text) element.Specification;

            //foreach (var textPlaceholderReplacement in element.TextPlaceholderReplacements)
            //{
            //    switch (textPlaceholderReplacement.TextPlaceholder)
            //    {
            //        case TextPlaceholder.Pagenumber:
            //            text = text.Replace(textPlaceholderReplacement.Replacement, pageNumber.ToString());
            //            break;
            //        case TextPlaceholder.TotalNumberOfPages:
            //            text = text.Replace(textPlaceholderReplacement.Replacement, totalPageNumbers.ToString());
            //            break;
            //        default:
            //            throw new ArgumentOutOfRangeException();
            //    }
            //}


            //var fillCharWidth = Unit.Zero;
            //if (!string.IsNullOrEmpty(element.Filling))
            //    fillCharWidth = text.Font.CalculateTextWidth(element.Filling);

            gfx.DrawString(specification.TextContent,
                           ((PdfSharpFont)specification.Font).XFont,
                           new XSolidBrush(XColor.FromArgb(specification.TextColor)),
                           new XRect(element.InnerBox.Left.Points, element.InnerBox.Top.Points, 10, element.InnerBox.Height.Points),
                           XStringFormats.TopLeft);


            //var y = element.InnerTop.Points;
            //var lineHeight = 0.cm();
            //foreach (var lineFromText in ((TextBehavior)specification.Behavior).GetLines(specification.TextContent, specification.Font, element.InnerWidth))
            //{
            //    if (lineHeight < element.ViewportTop || (element.ViewportBottom != 0.cm() && lineHeight > element.ViewportBottom))
            //    {
            //        lineHeight += specification.Font.CalculateLineHeight();
            //        continue;
            //    }
                    

            //    var line = lineFromText;
            //    //if (!string.IsNullOrEmpty(element.Filling))
            //    //{
            //    //    var currentTextWidth = text.Font.CalculateTextWidth(line);

            //    //    while (currentTextWidth + fillCharWidth < element.InnerWidth)
            //    //    {
            //    //        line += element.Filling;
            //    //        currentTextWidth += fillCharWidth;
            //    //    }
            //    //}

            //    double x;
            //    switch (specification.Alignment)
            //    {
            //        case Alignment.Center:
            //            x = (element.InnerWidth - specification.Font.CalculateTextWidth(line)).Points / 2;
            //            break;
            //        case Alignment.Justify:
            //            throw new NotImplementedException("Justification not yet implemented");
            //        case Alignment.Left:
            //            x = 0;
            //            break;
            //        case Alignment.Right:
            //            x = (element.InnerWidth - specification.Font.CalculateTextWidth(line)).Points;
            //            break;
            //        default:
            //            throw new ArgumentOutOfRangeException();
            //    }

            //    gfx.DrawString(line, 
            //                   ((PdfSharpFont)specification.Font).XFont, 
            //                   new XSolidBrush(XColor.FromArgb(specification.TextColor)), 
            //                   element.InnerLeft.Points + x, 
            //                   y,
            //                   XStringFormats.TopLeft);
            //    y += specification.Font.CalculateLineHeight().Points;
            //    lineHeight += specification.Font.CalculateLineHeight();
            //    //TODO: ADD LINE SPACING
            //}
        }
    }
}