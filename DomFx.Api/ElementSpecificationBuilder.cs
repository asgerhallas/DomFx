using DomFx.Layouters.Specification;

namespace DomFx.Api
{
    public interface ElementSpecificationBuilder<in TSelf> : ElementBuilder
    {
        ElementSpecification Element { get; }
    }
}