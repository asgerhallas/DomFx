using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Behaviors
{
    public interface IWidthBehavior : IBehavior
    {
        void ApplyBeforeLining(IElement element);
    }
}