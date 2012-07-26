using DomFx.Layouters.Specification;

namespace DomFx.Layouters.Behaviors
{
    public class MaintainHeightAspectRatioBehavior : StandardHeightBehavior
    {
        readonly Image specification;

        public MaintainHeightAspectRatioBehavior(Image specification)
        {
            this.specification = specification;
        }

        protected override Unit CalculateHeight(LayoutedElement element)
        {
            return specification.Source.Height*(element.ForcedInnerWidth/specification.Source.Width);
        }

    }
}