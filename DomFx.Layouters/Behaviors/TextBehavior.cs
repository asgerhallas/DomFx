using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Behaviors
{
    public class TextBehavior : CompositeBehavior
    {
        public TextBehavior(Text specification)
            : base(new TextHeightBehavior(specification))
        {
        }
    }
}