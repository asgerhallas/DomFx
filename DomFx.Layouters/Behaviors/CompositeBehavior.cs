using System.Linq;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Behaviors
{
    public abstract class CompositeBehavior
    {
        readonly IBehavior[] behaviors;

        protected CompositeBehavior(params IBehavior[] behaviors)
        {
            this.behaviors = behaviors;
        }

        void ApplyInternal<TBehavior, TElement>(TElement element) where TBehavior : IBehavior<TElement>
        {
            foreach (var behavior in behaviors.OfType<TBehavior>())
            {
                behavior.Behave(element);
            }
        }

        public void Apply<TBehavior>(LayoutedElement element) where TBehavior : IBehavior<LayoutedElement>
        {
            ApplyInternal<TBehavior, LayoutedElement>(element);
        }

        public void Apply<TBehavior>(IElement element) where TBehavior : IBehavior<IElement>
        {
            ApplyInternal<TBehavior, IElement>(element);
        }
    }
}