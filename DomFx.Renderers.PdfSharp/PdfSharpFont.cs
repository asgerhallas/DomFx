using System.Linq;
using System.Reflection;
using System.Windows.Media;
using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Style;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace DomFx.Renderers.PdfSharp.WPF
{
    public class PdfSharpFont : IFont
    {
        public XFont XFont { get; private set; }

        public PdfSharpFont(string family, double size) 
            : this(family, size, XFontStyle.Regular, new XPdfFontOptions(PdfFontEmbedding.Always))
        {
        }

        public PdfSharpFont(string family, double size, XFontStyle style, XPdfFontOptions options)
        {
            XFont = new XFont(family, size, style, options);
            //var typeface = (Typeface)XFont.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Single(x => x.Name == "typeface").GetValue(XFont);
            //typeface.TryGetGlyphTypeface(out glyphTypeface);
        }

        public string Family
        {
            get { return XFont.FontFamily.Name; }
        }

        public double Size
        {
            get { return XFont.Size; }
        }

        public Unit CalculateTextHeight(string text, Unit width)
        {
            return 10.cm();
        }

        public Unit CalculateLineHeight()
        {
            return XFont.Height.points();
        }
    }
}