using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;

namespace DomFx.Api
{
    public interface ElementSpecificationBuilder<in TSelf> : ElementBuilder
    {
        IElement Element { get; }
    }
}