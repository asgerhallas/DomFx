using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification;

namespace DomFx.Tests.Fakes
{
    public class TestBehaviors : Behaviors
    {
        public TestBehaviors(params Behavior[] behaviors) : base(behaviors)
        {
        }
    }
}