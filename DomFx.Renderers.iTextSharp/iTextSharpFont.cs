using DomFx.Layouters;
using DomFx.Layouters.Specification.Style;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DomFx.Renderers.iTextSharp
{
    public class iTextSharpFont : IFont
    {
        readonly double leading;

        public iTextSharpFont(BaseFont font, double size, double leading)
        {
            this.leading = leading;
            Font = new Font(font, (float) size);
        }

        public iTextSharpFont(Font font, double leading)
        {
            this.leading = leading;
            Font = font;
        }

        public double Leading
        {
            get { return leading; }
        }

        public Font Font { get; private set; }

        public string Family
        {
            get { return Font.Familyname; }
        }

        public double Size
        {
            get { return Font.Size; }
        }

        public Unit CalculateTextHeight(string text, Unit width)
        {
            var ct = new ColumnText(null) { Leading = (float) leading };
            ct.AddText(new Phrase(text, Font));
            ct.SetSimpleColumn(0, 0, (float) width.Points, float.MaxValue);
            ct.Go(true);
            var test = ct.LinesWritten*ct.Leading.points();
            return test;
        }

        public Unit CalculateLineHeight()
        {
            return Unit.FromPoints(leading);
        }
    }
}