using System.Linq;
using DomFx.Layouters.Specification;

namespace DomFx.Layouters.Behaviors
{
    public abstract class Behaviors
    {
        readonly Behavior[] behaviors;

        protected Behaviors(params Behavior[] behaviors)
        {
            this.behaviors = behaviors;
        }

        void ApplyInternal<TBehavior, TElement>(TElement element) where TBehavior : Behavior<TElement>
        {
            foreach (var behavior in behaviors.OfType<TBehavior>())
            {
                behavior.Behave(element);
            }
        }

        public void Apply<TBehavior>(LayoutedElement element) where TBehavior : Behavior<LayoutedElement>
        {
            ApplyInternal<TBehavior, LayoutedElement>(element);
        }

        public void Apply<TBehavior>(ElementSpecification element) where TBehavior : Behavior<ElementSpecification>
        {
            ApplyInternal<TBehavior, ElementSpecification>(element);
        }
    }
}