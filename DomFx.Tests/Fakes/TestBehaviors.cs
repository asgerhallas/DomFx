using DomFx.Layouters.Behaviors;

namespace DomFx.Tests.Fakes
{
    public class TestBehaviors : CompositeBehavior
    {
        public TestBehaviors(params IBehavior[] behaviors) : base(behaviors)
        {
        }
    }
}