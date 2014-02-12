using DomFx.Layouters;
using DomFx.Layouters.Specification.Style;

namespace DomFx.Api.Builder.Generator
{
    public class NullFont : IFont 
    {
        public string Family { get; private set; }
        public double Size { get; private set; }

        public Unit CalculateTextHeight(string text, Unit width)
        {
            return Unit.Zero;
        }

        public Unit CalculateLineHeight()
        {
            return Unit.Zero;
        }
    }
}