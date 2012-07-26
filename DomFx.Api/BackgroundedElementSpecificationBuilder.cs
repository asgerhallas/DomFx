using DomFx.Layouters.Specification;

namespace DomFx.Api
{
    public interface BackgroundedElementSpecificationBuilder<in TSelf>
    {
        Backgrounded Element { get; }
    }
}