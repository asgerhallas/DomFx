using DomFx.Layouters;
using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification;

namespace DomFx.Tests.Fakes
{
    public class TestHeightBehavior : StandardHeightBehavior
    {
        protected override Unit CalculateHeight(LayoutedElement element)
        {
            return 5.cm();
        }
    }
}