using DomFx.Layouters;
using DomFx.Layouters.Specification;

namespace DomFx.Tests.Fakes
{
    public class TestImageSource : IImageSource
    {
        readonly Unit width;
        readonly Unit height;

        public TestImageSource(Unit width, Unit height)
        {
            this.width = width;
            this.height = height;
        }

        public Unit Width
        {
            get { return width; }
        }

        public Unit Height
        {
            get { return height; }
        }
    }
}