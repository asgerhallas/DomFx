using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Behaviors
{
    public abstract class StandardWidthBehavior : IWidthBehavior
    {
        public void Behave(IElement element)
        {
            if (element.InnerWidth.IsDefined)
                return;

            element.InnerWidth = CalculateWidth(element);
        }

        protected abstract Unit CalculateWidth(IElement element);
    }
}