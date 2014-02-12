using System.Linq;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Behaviors
{
    public class CompositeBehavior : IHeightBehavior, IWidthBehavior
    {
        readonly IBehavior[] behaviors;

        protected CompositeBehavior(params IBehavior[] behaviors)
        {
            this.behaviors = behaviors;
        }

        public void ApplyBeforePaging(LayoutedElement element)
        {
            foreach (var behavior in behaviors.OfType<IHeightBehavior>())
            {
                behavior.ApplyBeforePaging(element);
            }
        }

        public void ApplyBeforeLining(IElement element)
        {
            foreach (var behavior in behaviors.OfType<IWidthBehavior>())
            {
                behavior.ApplyBeforeLining(element);
            }
        }
    }
}