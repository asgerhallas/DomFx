using DomFx.Layouters.Specification;

namespace DomFx.Layouters.Behaviors
{
    public class TextBehavior : Behaviors
    {
        public TextBehavior(Text specification)
            : base(new TextHeightBehavior(specification))
        {
        }
    }
}