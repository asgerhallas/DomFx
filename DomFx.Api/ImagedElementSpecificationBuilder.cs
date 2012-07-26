using DomFx.Layouters.Specification;

namespace DomFx.Api
{
    public interface ImagedElementSpecificationBuilder<TSelf> : ElementBuilder
    {
        Imaged Element { get; }
    }
}