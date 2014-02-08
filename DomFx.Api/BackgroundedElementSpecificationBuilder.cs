using DomFx.Layouters.Specification;

namespace DomFx.Api
{
    public interface BackgroundedElementSpecificationBuilder<in TSelf>
    {
        IBackgrounded Element { get; }
    }
}