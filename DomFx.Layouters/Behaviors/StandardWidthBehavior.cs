using DomFx.Layouters.Specification;

namespace DomFx.Layouters.Behaviors
{
    public abstract class StandardWidthBehavior : WidthBehavior
    {
        public void Behave(ElementSpecification element)
        {
            if (element.InnerWidth.IsDefined)
                return;

            element.InnerWidth = CalculateWidth(element);
        }

        protected abstract Unit CalculateWidth(ElementSpecification element);
    }
}