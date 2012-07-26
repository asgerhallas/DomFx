using DomFx.Layouters.Specification;

namespace DomFx.Layouters.Behaviors
{
    public class MaintainWidthAspectRatioBehavior : StandardWidthBehavior
    {
        readonly Image specification;

        public MaintainWidthAspectRatioBehavior(Image specification)
        {
            this.specification = specification;
        }

        protected override Unit CalculateWidth(ElementSpecification element)
        {
            if (element.InnerHeight.IsDefined)
                return specification.Source.Width * (element.InnerHeight / specification.Source.Height);

            return element.InnerWidth;
        }
    }
}