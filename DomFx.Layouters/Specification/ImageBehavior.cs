using DomFx.Layouters.Behaviors;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Specification
{
    public class ImageBehavior : CompositeBehavior
    {
        public ImageBehavior(Image specification)
            : base(new MaintainHeightAspectRatioBehavior(specification),
                   new MaintainWidthAspectRatioBehavior(specification))
        {
        }
    }
}