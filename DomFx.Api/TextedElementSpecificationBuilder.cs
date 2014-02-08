using DomFx.Layouters.Specification;

namespace DomFx.Api
{
    public interface TextedElementSpecificationBuilder<in TSelf>
    {
        ITexted Element { get; }
    }
}