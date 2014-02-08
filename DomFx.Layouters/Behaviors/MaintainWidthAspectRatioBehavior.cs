using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Behaviors
{
    public class MaintainWidthAspectRatioBehavior : StandardWidthBehavior
    {
        readonly Image specification;

        public MaintainWidthAspectRatioBehavior(Image specification)
        {
            this.specification = specification;
        }

        protected override Unit CalculateWidth(IElement element)
        {
            if (element.InnerHeight.IsDefined)
                return specification.Source.Width * (element.InnerHeight / specification.Source.Height);

            return element.InnerWidth;
        }
    }
}