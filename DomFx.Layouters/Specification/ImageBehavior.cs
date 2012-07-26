using DomFx.Layouters.Behaviors;

namespace DomFx.Layouters.Specification
{
    public class ImageBehavior : Behaviors.Behaviors
    {
        public ImageBehavior(Image specification)
            : base(new MaintainHeightAspectRatioBehavior(specification),
                   new MaintainWidthAspectRatioBehavior(specification))
        {
        }
    }
}