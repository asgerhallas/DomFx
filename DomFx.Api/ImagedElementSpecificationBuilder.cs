using DomFx.Layouters.Specification;

namespace DomFx.Api
{
    public interface ImagedElementSpecificationBuilder<TSelf> : ElementBuilder
    {
        IImaged Element { get; }
    }
}