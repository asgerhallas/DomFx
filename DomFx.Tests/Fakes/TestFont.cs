using DomFx.Layouters;
using DomFx.Layouters.Specification;

namespace DomFx.Tests.Fakes
{
    public class TestFont : Font
    {
        public string Family { get { return "Hallas"; }}

        public double Size { get { return 12; }}

        public Unit CalculateTextWidth(string text)
        {
            return text.Length*1.cm();
        }

        public Unit CalculateTextHeight(string text, Unit width)
        {
            return 1.cm();
        }

        public Unit CalculateLineHeight()
        {
            return 1.cm();
        }
    }
}