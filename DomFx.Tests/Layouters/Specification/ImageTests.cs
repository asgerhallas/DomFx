using DomFx.Layouters;
using DomFx.Layouters.Specification;
using DomFx.Layouters.Specification.Element;
using DomFx.Tests.Fakes;
using Xunit;

namespace DomFx.Tests.Layouters.Specification
{
    public class ImageTests : LiningTestsBase
    {
        [Fact]
        public void image_with_no_width_and_no_height_will_get_parent_width_and_maintain_aspect_ratio()
        {
            Element(innerWidth: 2.cm(), innerHeight: 5.cm(), children: new[]
            {
                ChildElement(behavior: new ImageBehavior(new Image(new TestImageSource(10.cm(), 20.cm()))))
            });

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 2.cm(), height: 4.cm());
        }

        [Fact]
        public void image_with_width_and_no_height_will_maintain_aspect_ratio()
        {
            Element(innerWidth: 2.cm(), innerHeight: 5.cm(), children: new[]
            {
                ChildElement(innerWidth: 1.cm(), behavior: new ImageBehavior(new Image(new TestImageSource(10.cm(), 20.cm()))))
            });

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 1.cm(), height: 2.cm());
        }

        [Fact]
        public void image_with_no_width_but_height_will_maintain_aspect_ratio()
        {
            Element(innerWidth: 2.cm(), innerHeight: 5.cm(), children: new[]
            {
                ChildElement(innerHeight: 2.cm(), behavior: new ImageBehavior(new Image(new TestImageSource(10.cm(), 20.cm()))))
            });

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 1.cm(), height: 2.cm());
        }

        [Fact]
        public void image_with_width_and_height_will_not_maintain_aspect_ratio()
        {
            Element(innerWidth: 2.cm(), innerHeight: 5.cm(), children: new[]
            {
                ChildElement(innerWidth: 1.cm(), innerHeight: 4.cm(), behavior: new ImageBehavior(new Image(new TestImageSource(10.cm(), 20.cm()))))
            });

            LayoutWithPageWidth(10.cm());

            Element(2).ShouldBeAt(top: 0.cm(), left: 0.cm(), width: 1.cm(), height: 4.cm());
        }
    }
}