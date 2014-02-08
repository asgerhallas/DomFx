using DomFx.Layouters.Specification.Element;

namespace DomFx.Layouters.Specification
{
    public interface IImaged : IElement
    {
        IImageSource Source { get; set; }
        void SizeBySource();
    }
}