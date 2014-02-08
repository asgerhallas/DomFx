using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification;

namespace DomFx.Tests.Fakes
{
    public class TestBehaviors : CompositeBehavior
    {
        public TestBehaviors(params IBehavior[] behaviors) : base(behaviors)
        {
        }
    }
}